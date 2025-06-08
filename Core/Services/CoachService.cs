using AutoMapper;
using Domain.Constants;
using Domain.Contracts;
using Domain.Entities;
using Domain.ValueObjects;
using Services.Abstractions;
using Shared;

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

        public async Task<bool> RequestToBecomeCoachAsync(int gymId, HashSet<WorkDayDto> workDaysDto)
        {
            var coachId = _userServices.Id!;
            var gymRepo = await _unitOfWork.GetRepositories<Gym, int>().GetByIdAsync(gymId);
            if (gymRepo == null)
                return false;


            var request = new GymCoach
            {
                GymId = gymId,
                CoachId = coachId.Value,
                WorkDays = _mapper.Map<ICollection<WorkDay>>(workDaysDto),
            };

            _unitOfWork.GetRepositories<GymCoach, int>().Insert(request);
            var result = await _unitOfWork.CompleteSaveAsync();
            return result;

        }

        public async Task<bool> CreateDietAsync(int traineeId, MealScheduleDto dietDto)
        {

            var trainee = await _unitOfWork.GetRepositories<Trainee, int>().GetByIdAsync(traineeId);
            if (trainee == null)
            {
                //throw new NotFoundException($"Trainee with ID {traineeId} not found.");
                return false;
            }

            var mealSchedule = _mapper.Map<MealSchedule>(dietDto);
            mealSchedule.CoachId = _userServices.Id!.Value; // will do it later abo  Salem 
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
    }
}
