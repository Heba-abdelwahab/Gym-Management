using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Services.Abstractions;
using Services.MappingProfiles;
using Services.Specifications;
using Shared;
using Shared.TraineeGym;

namespace Services
{
    internal class GymOwnerService : IGymOwnerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<GymOwner, int> _ownerRepo;
        public GymOwnerService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _ownerRepo = _unitOfWork.GetRepositories<GymOwner, int>();
        }

        public async Task<GymOwnerToReturnDto> GetGymOwnerInfo(int ownerId)
        {
            var gymOwner = await _ownerRepo.GetByIdWithSpecAsync(new GetGymOwnerInfoSpecification(ownerId));
            if (gymOwner == null)
            {
                throw new GymOwnerNotFoundException(ownerId);
            }

            var mappedOwner = _mapper.Map<GymOwnerToReturnDto>(gymOwner);
            return mappedOwner;
        }

        public async Task<IReadOnlyList<GymToReturnDto>> GetGymsForOwnerAsync(int ownerId)
        {
            var gymOwner = await _ownerRepo.GetByIdAsync(ownerId);
            if (gymOwner == null)
            {
                throw new GymOwnerNotFoundException(ownerId);
            }

            var gyms = await _unitOfWork.GetRepositories<Gym, int>().GetAllWithSpecAsync(new GetGymsForOwner(ownerId));
            var mappedGyms = _mapper.Map<IReadOnlyList<GymToReturnDto>>(gyms);

            return mappedGyms;
        }
    }
}
