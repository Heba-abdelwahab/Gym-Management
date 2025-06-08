using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Services.Abstractions;
using Services.Specifications;
using Shared;
using Domain.Exceptions;
using Services.MappingProfiles;
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
            var coachId = await _userServices.GetUserIdAsync();
            var gymRepo = await _unitOfWork.GetRepositories<Gym, int>().GetByIdAsync(gymId);
            if (gymRepo == null)
                return false;


            var request = new GymCoach
            {
                GymId = gymId,
                CoachId = coachId.Value!,
                WorkDays = _mapper.Map<ICollection<WorkDay>>(workDaysDto),
            };

            _unitOfWork.GetRepositories<GymCoach, int>().Insert(request);
            var result = await _unitOfWork.CompleteSaveAsync();
            return result;

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
            //Gym? gym = await _unitOfWork.GetRepositories<Gym, int>().GetByIdAsync(gymId);
            //if (gym == null)
            //    throw new GymNotFoundException(gymId);

            await _unitOfWork.GetRepositories<GymCoach, int>().GetByIdAsync(gymId);

        }
    }
}
