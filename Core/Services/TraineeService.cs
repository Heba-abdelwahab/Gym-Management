using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Constants;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Domain.ValueObjects;
using Services.Abstractions;
using Services.Specifications;
using Shared;

namespace Services;

internal sealed class TraineeService : ITraineeService
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userServices;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;

    public TraineeService(
        IAuthenticationService authenticationService,
        IUnitOfWork unitOfWork,
        IUserService userServices,
        IMapper mapper,
        ITokenService tokenService)
    {
        _authenticationService = authenticationService;
        _unitOfWork = unitOfWork;
        _userServices = userServices;
        _mapper = mapper;
        _tokenService = tokenService;

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
          (request.FirstName, request.LastName, request.UserName, request.Email, request.Password, Roles.Trainee);

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
                    trainee.Id, registerUser.UserName,
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

}
