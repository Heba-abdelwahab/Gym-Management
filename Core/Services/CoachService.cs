using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Services.Abstractions;
using Services.Specifications;
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

        public async Task<List<CoachToReturnDto>> GetCoachesbyGym(int gymId)
        {
            var coaches = await _unitOfWork.GetRepositories<Coach, int>()
                                           .GetAllWithSpecAsync(new GetCoaches(gymId));

            var result = _mapper.Map<List<CoachToReturnDto>>(coaches);

            return result;
        }

    }
}
