using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
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

        public CoachService( 
            IAuthenticationService authenticationService, 
            IUnitOfWork unitOfWork, 
            IUserService userServices,
            IMapper mapper)
        {
            _authenticationService = authenticationService;
            _unitOfWork = unitOfWork;
            _userServices = userServices;
            _mapper = mapper;
        }

        public async Task<bool> RequestToBecomeCoachAsync(int gymId, HashSet<WorkDayDto> workDaysDto)
        {
            var coachId = _userServices.Id;
            var gymRepo = await _unitOfWork.GetRepositories<Gym, int>().GetByIdAsync(gymId);
            if (gymRepo == null)
                return false;


            var request = new GymCoach
            {
                GymId = gymId,
                CoachId = coachId!,
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
            mealSchedule.CoachId = _userServices.Id;
            mealSchedule.TraineeId = traineeId;

            _unitOfWork.GetRepositories<MealSchedule, int>().Insert(mealSchedule);

            return await _unitOfWork.CompleteSaveAsync();
        }

    }
}
