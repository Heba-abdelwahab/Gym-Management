using AutoMapper;
using Domain.Constants;
using Domain.Contracts;
using Domain.Entities;
using Domain.ValueObjects;
using Services.Abstractions;
using Services.Specifications;
using Shared;
using Domain.Exceptions;
using Services.MappingProfiles;
using Domain.Enums;
namespace Services
{
    public class CoachService : ICoachService
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userServices;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;


        public CoachService(
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

        public async Task<bool> RequestToBecomeCoachAsync(CoachRequestGymDto coachRequest)
        {
            var coachId = _userServices.Id;
            var gymRepo = await _unitOfWork.GetRepositories<Gym, int>().GetByIdAsync(coachRequest.gymId);
            if (gymRepo == null)
                return false;


            var request = new GymCoach
            {
                GymId = coachRequest.gymId,
                CoachId = coachId!.Value,
                WorkDays = _mapper.Map<ICollection<WorkDay>>(coachRequest.workDayDtos),
            };

            _unitOfWork.GetRepositories<GymCoach, int>().Insert(request);
            var result = await _unitOfWork.CompleteSaveAsync();
            return result;

        }

        public async Task<List<CoachToReturnDto>> GetCoachesbyGym(int gymId)
        {
           
            var coaches = await _unitOfWork.GetRepositories<Coach, int>()
                                           .GetAllWithSpecAsync(new GetCoaches(gymId));
            if(!coaches.Any())
            {
                throw new GymNotFoundException(gymId);
            }
            var result = _mapper.Map<List<CoachToReturnDto>>(coaches);

            return result;
        }


        public async Task<bool> CreateDietAsync(int traineeId, MealScheduleDto dietDto)
        {
            var coachId = _userServices.Id;

            var trainee = await _unitOfWork.GetRepositories<Trainee, int>().GetByIdAsync(traineeId);
            if (trainee is null)
            {
                throw new TraineeNotFoundException(traineeId);
            }

            var authorizedCoach = await IsCoachAuthorizedToAccessTraineeAsync(coachId!.Value, trainee);

            if (!authorizedCoach)
                throw new Exception("Un authorized Coach to access this trainee");

            var mealSchedule = _mapper.Map<MealSchedule>(dietDto);
            mealSchedule.CoachId = coachId!.Value;
            mealSchedule.TraineeId = traineeId;

            _unitOfWork.GetRepositories<MealSchedule, int>().Insert(mealSchedule);

            return await _unitOfWork.CompleteSaveAsync();
        }

        public async Task<AuthCoachResultDto> CreateCoachAsync(RegisterCoachDto request)
        {
            var registerUser = new RegisterUserDto
           (request.FirstName, request.LastName, request.UserName, request.Email, request.Password, Roles.Coach);

            var authResult = await _authenticationService.RegisterUserAsync(registerUser);


            var coach = new Coach
            {
                AppUserId = authResult.AppUserId,
                Address = _mapper.Map<Address>(request.Address),
                DateOfBirth = request?.DateOfBirth

            };

            _unitOfWork.GetRepositories<Coach, int>().Insert(coach);


            if (await _unitOfWork.CompleteSaveAsync())
            {

                var coachClaims = _tokenService.GenerateAuthClaims(
                        coach.Id, registerUser.UserName,
                         registerUser.Email, registerUser.Role);

                return new AuthCoachResultDto(
                    authResult.UserName,
                    _tokenService.GenerateAccessToken(coachClaims));

            }

            return null!;

        }
        public async Task<IEnumerable<CoachPendingDto>> GetGymPendingCoachs(int gymId)
        {
            Gym? gym = await _unitOfWork.GetRepositories<Gym, int>().GetByIdAsync(gymId);
            if (gym == null)
                throw new GymNotFoundException(gymId);

            IRepository<Coach,int> coachRepo= _unitOfWork.GetRepositories<Coach, int>();
            IEnumerable<Coach> coachs = await coachRepo.GetAllWithSpecAsync(new GetGymPendingCoachsSpec(gymId));

            IEnumerable<CoachPendingDto> coachPendingDtos= _mapper.Map<IEnumerable<CoachPendingDto>>(coachs);

            return coachPendingDtos;
        }

        public async Task HandleCoachJobRequest(int gymId, HandleJobRequestDto jobRequestDto)
        {

            
            var gymCoach= await _unitOfWork.GetRepositories<GymCoach, int>().GetByIdWithSpecAsync(new GetGymCoachSpec(gymId,jobRequestDto.CoachId));
            if (gymCoach == null)
                throw new GymCoachNotFoundException(gymId, jobRequestDto.CoachId);

            gymCoach.Status = jobRequestDto.IsAccepted==true ? RequestStatus.Accepted : RequestStatus.Rejected;

            if (!await _unitOfWork.CompleteSaveAsync())
                throw new Exception("fail to update the job request status");
        }

        public async Task<bool> CreateExerciseScheduleAsync(int traineeId, ExerciseScheduleDto exerciseScheduleDto)
        {
           var coachId = _userServices.Id;

            var trainee = await _unitOfWork.GetRepositories<Trainee, int>().GetByIdAsync(traineeId);
            if (trainee is null)
            {
                throw new TraineeNotFoundException(traineeId);
            }
            var authorizedCoach = await IsCoachAuthorizedToAccessTraineeAsync(coachId!.Value, trainee);

            if (!authorizedCoach)
                throw new Exception("Un authorized Coach to access this trainee");

            var exerciseSchedule = _mapper.Map<ExercisesSchedule>(exerciseScheduleDto);

            exerciseSchedule.CoachId = coachId!.Value;
            exerciseSchedule.TraineeId = traineeId;

            _unitOfWork.GetRepositories<ExercisesSchedule, int>().Insert(exerciseSchedule);

            return await _unitOfWork.CompleteSaveAsync();
        }

        public async Task<bool> IsCoachAuthorizedToAccessTraineeAsync(int coachId, Trainee trainee)
        {
           
            if (trainee?.GymId is null)
                throw new Exception("Trainee has not joined any gym yet.");

            var coachGyms = await _unitOfWork.GetRepositories<GymCoach, int>().GetAllWithSpecAsync(new GymCoachesSpec(coachId));

            var coachGymIds = coachGyms.Select(gc => gc.GymId).ToList();

            if (!coachGymIds.Contains(trainee.GymId.Value))
                throw new Exception("The coach and the trainee are not in the same gym.");


            if (trainee?.CoachId is null)
                throw new Exception("Trainee has not been assigned to any coach.");

            return trainee.CoachId.Value == coachId; 
        }
    }
}
