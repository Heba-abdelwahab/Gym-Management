﻿using AutoMapper;
using CloudinaryDotNet;
using Domain.Constants;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Services.Abstractions;
using Services.Specifications;
using Services.Specifications.TraineeSpec;
using Shared;
using Shared.Auth;
using Shared.Trainee;
using Shared.TraineeGym;

namespace Services;

internal sealed class TraineeService : ITraineeService
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userServices;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly IPaymentService _paymentService;
    private readonly IPhotoService _photoService;

    public TraineeService(
        IAuthenticationService authenticationService,
        IUnitOfWork unitOfWork,
        IUserService userServices,
        IMapper mapper,
        ITokenService tokenService,
        IPaymentService paymentService,
        IPhotoService photoService
        )    {
        _authenticationService = authenticationService;
        _unitOfWork = unitOfWork;
        _userServices = userServices;
        _mapper = mapper;
        _tokenService = tokenService;
        _paymentService = paymentService;
        _photoService = photoService;
    }
    public async Task<bool> AssignCoachToTrainee(AssignCoachToTraineeDto assignCoachToTrainee)
    {
        // Get the trainee
        Trainee trainee = await _unitOfWork.GetRepositories<Trainee, int>()
            .GetByIdAsync(assignCoachToTrainee.TraineeId);

        if (trainee == null)
            throw new TraineeNotFoundException(assignCoachToTrainee.TraineeId);

        // Get the new coach and gym coach
        Coach coach = await _unitOfWork.GetRepositories<Coach, int>()
            .GetByIdAsync(assignCoachToTrainee.CoachId);

        GymCoach gymCoach = await _unitOfWork.GetRepositories<GymCoach, int>()
            .GetByIdWithSpecAsync(new GymCoachesSpec(assignCoachToTrainee.CoachId));

        if (coach == null || gymCoach == null)
            throw new CoeachesNotFoundException(assignCoachToTrainee.CoachId);

        // Only proceed if this is a different coach assignment
        if (!trainee.CoachId.HasValue || trainee.CoachId.Value != assignCoachToTrainee.CoachId)
        {
            // Get the old gym coach if exists
            GymCoach oldGymCoach = null;
            if (trainee.CoachId.HasValue)
            {
                oldGymCoach = await _unitOfWork.GetRepositories<GymCoach, int>()
                    .GetByIdWithSpecAsync(new GymCoachesSpec(trainee.CoachId.Value));
            }

            // Update capacities only if this is a new assignment
            if (oldGymCoach != null&&oldGymCoach.CurrentCapcity>=0)
            {
                oldGymCoach.CurrentCapcity--;
                _unitOfWork.GetRepositories<GymCoach, int>().Update(oldGymCoach);
            }

            gymCoach.CurrentCapcity++;
            _unitOfWork.GetRepositories<GymCoach, int>().Update(gymCoach);
        }

        // Update trainee's coach
        trainee.CoachId = assignCoachToTrainee.CoachId;
        _unitOfWork.GetRepositories<Trainee, int>().Update(trainee);

        // Save changes
        return await _unitOfWork.CompleteSaveAsync();
    }
    public async Task<AuthTraineeResultDto> CreateTraineeAsync(RegisterTraineeDto request)
    {
        var registerUser = new RegisterUserDto
          (request.FirstName, request.LastName, request.UserName,
          request.Email, request.Password, request.PhoneNumber, Roles.Trainee);

        var authResult = await _authenticationService.RegisterUserAsync(registerUser);


        var trainee = new Trainee
        {
            AppUserId = authResult.AppUserId,
            Address = _mapper.Map<Address>(request.Address),
            Weight = request?.Weight,
            ReasonForJoining = request?.ReasonForJoining!,
            DateOfBirth = request?.DateOfBirth
        };

        _unitOfWork.GetRepositories<Trainee, int>().Insert(trainee);


        if (await _unitOfWork.CompleteSaveAsync())
        {

            var coachClaims = _tokenService.GenerateAuthClaims(
                    trainee.Id, trainee.AppUserId, registerUser.UserName,
                     registerUser.Email, registerUser.Role);

            return new AuthTraineeResultDto(
                authResult.UserName,
                _tokenService.GenerateAccessToken(coachClaims));

        }

        return null!;


    }

    public async Task<List<TraineeToReturnDto>> GetTrineesByGem(int gymId)
    {
        var Trainees = await _unitOfWork.GetRepositories<Trainee, int>().
            GetAllWithSpecAsync(new GetTraineeByGemSpec(gymId));
        if (!Trainees.Any())
        {
            throw new GymNotFoundException(gymId);
        }
        var result = _mapper.Map<List<TraineeToReturnDto>>(Trainees);

        return result;
    }


    // =================================== Gym Membership ===================================
    public async Task<IReadOnlyList<GymMembershipsDto>> GetAllMembershipsForGym(int GymId)
    {
        var Memberships = await _unitOfWork.GetRepositories<Membership, int>().
            GetAllWithSpecAsync(new MembershipSpec(GymId));

        var membershipsDto = _mapper.Map<IReadOnlyList<GymMembershipsDto>>(Memberships);
        if (membershipsDto is null)
            throw new GymNotFoundException(GymId);

        return membershipsDto;
    }

    // Assign Trainee To Membership
    public async Task<ClientSecretToReturnDto> AssignTraineeToMembership(int membershipId)
    {
        int? TraineeId = _userServices.Id;
        var Trainee = await _unitOfWork.GetRepositories<Trainee, int>().GetByIdAsync(TraineeId!.Value);

        if (Trainee == null)
            throw new TraineeNotFoundException(TraineeId.Value);

        // Check if the trainee already has an active membership
        if (Trainee.MembershipId.HasValue && Trainee.MembershipEndDate > DateTime.UtcNow)
            throw new TraineeAlreadyHasActiveMembershipException(TraineeId.Value);

        var membership = await _unitOfWork.GetRepositories<Membership, int>().GetByIdAsync(membershipId);

        if (membership == null)
            throw new MembershipNotFoundException(membershipId);

        // payment for the membership
        var ClientSecret = await _paymentService.ProcessPaymentAsync(TraineeId.Value, membership.Cost, PayFor.Membership, membershipId);

        if (ClientSecret is null)
            throw new PaymentFailedException("Payment For The Membership Failed, Please Try Again");

        Trainee.MembershipId = membershipId;
        Trainee.MembershipStartDate = DateTime.UtcNow;
        Trainee.MembershipEndDate = DateTime.UtcNow.AddDays(membership.Duration);
        Trainee.GymId = membership.GymId;

        var result = await _unitOfWork.CompleteSaveAsync();
        if (!result)
            throw new Exception("An error occurred while assigning the trainee to the membership.");
        return new ClientSecretToReturnDto { ClientSecret = ClientSecret };
    }

    // ================================= Gym Classes ===================================
    public async Task<IReadOnlyList<Shared.TraineeGym.ClassTraineeToReturnDto>> GetClassesByGym(int GymId)
    {
        var Spec = new ClassesForTraineeSpec(GymId);
        var classes = await _unitOfWork.GetRepositories<Class, int>().GetAllWithSpecAsync(Spec);

        if (classes is null)
            throw new GymNotFoundException(GymId);

        var classesDto = _mapper.Map<IReadOnlyList<Shared.TraineeGym.ClassTraineeToReturnDto>>(classes);
        return classesDto;
    }

    public async Task<ClientSecretToReturnDto> JoinClass(int classId)
    {
        int? TraineeId = _userServices.Id;
        var trainee = await _unitOfWork.GetRepositories<Trainee, int>().GetByIdAsync(TraineeId!.Value);

        var ClassSpec = new ClassWithTraineesSpec(classId);
        var classEntity = await _unitOfWork.GetRepositories<Class, int>().GetByIdWithSpecAsync(ClassSpec);

        if (trainee == null)
            throw new TraineeNotFoundException(TraineeId.Value);
        if (classEntity == null)
            throw new ClassNotFoundException(classId);

        // check if the trainee is already in the class
        if (classEntity.Trainees.Any(t => t.Id == trainee.Id))
            throw new TraineeAlreadyInClassException(trainee.Id, classId);

        if (classEntity.Trainees.Count >= classEntity.Capacity)
            throw new ClassFullException(classId);

        // payment for the class
        var ClientSecret = await _paymentService.ProcessPaymentAsync(TraineeId.Value, classEntity.Cost, PayFor.Class, classId);

        if (ClientSecret is null)
            throw new PaymentFailedException("Payment For The Class Failed, Please Try Again");

        classEntity.Trainees.Add(trainee);

        var result = await _unitOfWork.CompleteSaveAsync();
        if (!result)
            throw new Exception("An error occurred while joining the class.");

        return new ClientSecretToReturnDto { ClientSecret = ClientSecret };
    }


    // ================================= Gym Features ===================================

    // Get Gym Features
    public async Task<IReadOnlyList<GymFeatureToReturnDto>> GetGymFeatures(int gymId)
    {
        var FeatureSpec = new GymFeatureMembershipByGymIdSpec(gymId);
        var Features = (IReadOnlyList<GymFeature>)await _unitOfWork.GetRepositories<GymFeature, int>().GetAllWithSpecAsync(FeatureSpec);

        if (Features is null)
            throw new GymNotFoundException(gymId);

        var FeaturesMapped = _mapper.Map<IReadOnlyList<GymFeatureToReturnDto>>(Features);

        return FeaturesMapped;
    }

    // Assign Trainee To Feature
    public async Task<ClientSecretToReturnDto> AssignTraineeToFeature(int featureId, int count)
    {
        int? TraineeId = _userServices.Id;
        var trainee = await _unitOfWork.GetRepositories<Trainee, int>().GetByIdWithSpecAsync(new TraineeWithSelectedFeatures(TraineeId!.Value));

        var WholeFeatureSpec = new WholeFeature(featureId);
        var Feature = await _unitOfWork.GetRepositories<GymFeature, int>().GetByIdWithSpecAsync(WholeFeatureSpec);

        if (trainee == null)
            throw new TraineeNotFoundException(TraineeId.Value);

        if (Feature == null)
            throw new GymFeatureNotFoundException(featureId);

        // Payment for the feature
        var ClientSecret = await _paymentService.ProcessPaymentAsync(TraineeId.Value, Feature.Cost * count, PayFor.Feature, featureId);
        if (ClientSecret is null)
            throw new PaymentFailedException("Payment For The Feature Failed, Please Try Again");

        // Check if the trainee already has this feature selected with its count
        var existingFeature = trainee.TraineeSelectedFeatures.FirstOrDefault(f => f.GymFeatureId == featureId);
        if (existingFeature != null)
        {
            // If the feature already exists, update the count and total cost
            existingFeature.SessionCount += count;
            existingFeature.TotalCost = (double)(Feature.Cost * existingFeature.SessionCount);

            _unitOfWork.GetRepositories<TraineeSelectedFeature, int>().Update(existingFeature);
            var Result = await _unitOfWork.CompleteSaveAsync();

            ///if (Result)
            ///    return new TraineeFeatureToReturnDto()
            ///    {
            ///        Name = Feature.Feature.Name,
            ///        Count = existingFeature.SessionCount,
            ///        SessionCost = Feature.Cost,
            ///        TotalCost = existingFeature.TotalCost
            ///    };
            ///else
            ///    return null;
            
            if (!Result)
                throw new Exception("An error occurred while assigning the trainee to the feature.");

            return new ClientSecretToReturnDto { ClientSecret = ClientSecret };
        }

        trainee.TraineeSelectedFeatures.Add(new TraineeSelectedFeature
        {
            GymFeatureId = featureId,
            TraineeId = trainee.Id,
            SessionCount = count,
            TotalCost = (double)(Feature.Cost * count)
        });

        var result = await _unitOfWork.CompleteSaveAsync();
        ///if (result)
        ///    return new TraineeFeatureToReturnDto()
        ///    {
        ///        Name = Feature.Feature.Name,
        ///        Count = count,
        ///        SessionCost = Feature.Cost,
        ///        TotalCost = (double)(Feature.Cost * count)
        ///    };
        ///else
        ///    return null;
       
        if (!result)
            throw new Exception("An error occurred while assigning the trainee to the feature.");

        return new ClientSecretToReturnDto { ClientSecret = ClientSecret };

    }

    // ================================= Trainee Subscriptions ===================================

    public async Task<TraineeSubscriptionsToReturnDto> TraineeSubscriptions()
    {
        int? TraineeId =  _userServices.Id;
        var TraineeDataSpec = new AllTraineeDataSpec(TraineeId!.Value);
        var Trainee = await _unitOfWork.GetRepositories<Trainee, int>().GetByIdWithSpecAsync(TraineeDataSpec);

        if (Trainee == null)
            throw new TraineeNotFoundException(TraineeId.Value);

        var TraineeMapped = _mapper.Map<TraineeSubscriptionsToReturnDto>(Trainee);
        TraineeMapped.Features = _mapper.Map<IReadOnlyList<TraineeFeatureToReturnDto>>(Trainee.TraineeSelectedFeatures);
        TraineeMapped.Class = _mapper.Map<IReadOnlyList<Shared.TraineeGym.ClassTraineeToReturnDto>>(Trainee.Classes);
        TraineeMapped.GymData = _mapper.Map<GymToReturnDto>(Trainee.Gym);


        return TraineeMapped;
    }

    // ============================== Trainee Gyms ===================================
    public async Task<IReadOnlyList<GymToReturnDto>> AllGyms()
    {
        var gyms = await _unitOfWork.GetRepositories<Gym, int>().GetAllAsync();
        if (gyms is null)
            throw new GymNotFoundException(0); // Assuming 0 is an invalid gym ID
        var gymsMapped = _mapper.Map<IReadOnlyList<GymToReturnDto>>(gyms);
        return gymsMapped;
    }

    public async Task<GymToReturnDto> GetGymById(int gymId)
    {
        var gyms = await _unitOfWork.GetRepositories<Gym, int>().GetByIdAsync(gymId);
        if (gyms is null)
            throw new GymNotFoundException(0); // Assuming 0 is an invalid gym ID
        var gymsMapped = _mapper.Map<GymToReturnDto>(gyms);
        return gymsMapped;
    }
    ///   mapping exception**********************
    public async Task<IReadOnlyList<ClassGymWithCoachToReturnDto>> GetAllClasses()
    {
        var classesSpec = new ClassWithCoachAndGymSpec();
        var classes = await _unitOfWork.GetRepositories<Class, int>().GetAllWithSpecAsync(classesSpec);
        if (classes is null)
            throw new GymNotFoundException(0); 
        var classesMapped = _mapper.Map<IReadOnlyList<ClassGymWithCoachToReturnDto>>(classes);
        return classesMapped;
    }

    public async Task<TraineeCoachToReturnDto?> GetTraineeCoach()
    {
        int? TraineeId = _userServices.Id;
        var TraineeCoachSpec = new GetTraineeCoach(TraineeId!.Value);
        var Trainee = await _unitOfWork.GetRepositories<Trainee, int>().GetByIdWithSpecAsync(TraineeCoachSpec);

        if (Trainee is null)
            throw new TraineeNotFoundException(TraineeId.Value);
        if (Trainee.Coach is null)
            return null;
        var CoachMapped = _mapper.Map<TraineeCoachToReturnDto>(Trainee.Coach);
        CoachMapped.TraineeCount = Trainee.Coach!.Trainees.Count;
        return CoachMapped;

    }

    // ================================= Trainee Profile ===================================
    public async Task<TraineeDataToReturnDto> GetTraineeProfile()
    {
        int? TraineeId = _userServices.Id;
        var TraineeSpec = new GetTraineeWithAppUserSpec(TraineeId!.Value);
        var Trainee = await _unitOfWork.GetRepositories<Trainee, int>().GetByIdWithSpecAsync(TraineeSpec);
        if (Trainee == null)
            throw new TraineeNotFoundException(TraineeId.Value);

        var mapped = _mapper.Map<TraineeDataToReturnDto>(Trainee);
        return mapped;
    }

    // ========================= Edit Trainee Profile ================================
    public async Task<TraineeDataToReturnDto> EditTraineeProfile(EditTraineeProfileDto editTraineeProfileDto)
    {
        int? TraineeId = _userServices.Id;
        var TraineeSpec = new GetTraineeWithAppUserSpec(TraineeId!.Value);
        var Trainee = await _unitOfWork.GetRepositories<Trainee, int>().GetByIdWithSpecAsync(TraineeSpec);
        if (Trainee == null)
            throw new TraineeNotFoundException(TraineeId.Value);
        Trainee.AppUser.FirstName = editTraineeProfileDto.FirstName;
        Trainee.AppUser.LastName = editTraineeProfileDto.LastName;
        Trainee.Address = _mapper.Map<Address>(editTraineeProfileDto.Address);
        Trainee.DateOfBirth = editTraineeProfileDto.DateOfBirth;
        Trainee.ReasonForJoining = editTraineeProfileDto.ReasonForJoining;
        Trainee.AppUser.PhoneNumber = editTraineeProfileDto.PhoneNumber;
        Trainee.Weight = editTraineeProfileDto.Weight;

        if (editTraineeProfileDto.Image != null)
        {

            var photoResult = await _photoService.AddPhotoFullPathAsync(editTraineeProfileDto.Image);
            Trainee.ImageUrl = photoResult.SecureUrl.AbsoluteUri;

            var photo = new Photo
            {
                Url = photoResult.SecureUrl.AbsoluteUri,
                PublicId = photoResult.PublicId
            };
            if (Trainee.AppUser.Photos.Count == 0)
                photo.IsMain = true;

            Trainee.AppUser.Photos.Add(photo);
        }

        _unitOfWork.GetRepositories<Trainee, int>().Update(Trainee);

        if (await _unitOfWork.CompleteSaveAsync())
        {
            return _mapper.Map<TraineeDataToReturnDto>(Trainee);
        }
        throw new Exception("An error occurred while updating the trainee profile.");
    }

    public async Task<TraineeInfoResultDto> GetTraineeByUserName(string username)
    {
        var trainee = await _unitOfWork.GetRepositories<Trainee, int>()
               .GetByIdWithSpecAsync(new GetTraineeByAppUserIdSpec(_userServices.AppUserId!));



        return _mapper.Map<TraineeInfoResultDto>(trainee);

    }

    public async Task<IReadOnlyList<ExerciseScheduleResultDto>> GetExerciseScheduleAsync()
    {
        int? traineeId = _userServices.Id;

        var schedule = await _unitOfWork.GetRepositories<ExercisesSchedule, int>().GetAllWithSpecAsync(new GetExerciseSchedulesForTraineeSpec(traineeId!.Value));
        return _mapper.Map<IReadOnlyList<ExerciseScheduleResultDto>>(schedule);
    }

    public async Task<IReadOnlyList<MealScheduleResultDto>> GetDietAsync()
    {
        int? traineeId = _userServices.Id;

        var diet = await _unitOfWork.GetRepositories<MealSchedule, int>().GetAllWithSpecAsync(new GetDietsForTraineeSpec(traineeId!.Value));
        return _mapper.Map<IReadOnlyList<MealScheduleResultDto>>(diet);
    }
}
