using AutoMapper;
using Domain.Constants;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Services.Abstractions;
using Services.Specifications;
using Services.Specifications.GymOwnerSpec;
using Shared;
using Shared.Auth;
using Shared.GymOwner;
using Shared.TraineeGym;

namespace Services
{
    internal class GymOwnerService : IGymOwnerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<GymOwner, int> _ownerRepo;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IAuthenticationService _authenticationService;



        public GymOwnerService(
            IAuthenticationService authenticationService,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IUserService userService,
            ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _ownerRepo = _unitOfWork.GetRepositories<GymOwner, int>();
            _userService = userService;
            _tokenService = tokenService;
            _authenticationService = authenticationService;
        }

        public async Task<GymOwnerToReturnDto> GetGymOwnerInfo()
        {
            int ownerId = _userService.Id.Value;
            var gymOwner = await _ownerRepo.GetByIdWithSpecAsync(new GetGymOwnerInfoSpecification(ownerId));
            if (gymOwner == null)
            {
                throw new GymOwnerNotFoundException(ownerId);
            }

            var mappedOwner = new GymOwnerToReturnDto()
            {
                FirstName = gymOwner.AppUser.FirstName,
                LastName = gymOwner.AppUser.LastName,
                UserName = gymOwner.AppUser.UserName,
                PhoneNumber = gymOwner.AppUser.PhoneNumber,
                Email = gymOwner.AppUser.Email
            };
           
            return mappedOwner;
        }

        public async Task<IReadOnlyList<GymToReturnDto>> GetGymsForOwnerAsync()
        {
            int ownerId = _userService.Id.Value;
            var gymOwner = await _ownerRepo.GetByIdAsync(ownerId);
            if (gymOwner == null)
            {
                throw new GymOwnerNotFoundException(ownerId);
            }

            var gyms = await _unitOfWork.GetRepositories<Gym, int>().GetAllWithSpecAsync(new GetGymsForOwner(ownerId));
            var mappedGyms = _mapper.Map<IReadOnlyList<GymToReturnDto>>(gyms);

            return mappedGyms;
        }


        public async Task<GymOwnerInfoResultDto> GetGymOwnerByUserNameAsync(string username)
        {
            var gymOwner = await _unitOfWork.GetRepositories<GymOwner, int>()
                     .GetByIdWithSpecAsync(new GetGymOwnerByAppUserIdSpec(_userService.AppUserId!));



            return _mapper.Map<GymOwnerInfoResultDto>(gymOwner);
        }



        public async Task<AuthAdminResultDto> CreateGymOwnerAsync(RegisterUserDto request)
        {

            var registerUser = new RegisterUserDto // need to change later
                (request.FirstName, request.LastName, request.UserName,
                request.Email, request.Password, request.PhoneNumber, Roles.Owner);

            var authResult = await _authenticationService.RegisterUserAsync(registerUser);


            var gymOwner = new GymOwner
            {
                AppUserId = authResult.AppUserId,
            };

            _unitOfWork.GetRepositories<GymOwner, int>().Insert(gymOwner);


            if (await _unitOfWork.CompleteSaveAsync())
            {

                var adminClaims = _tokenService.GenerateAuthClaims(
                        gymOwner.Id, gymOwner.AppUserId, registerUser.UserName,
                         registerUser.Email, registerUser.Role);

                return new AuthAdminResultDto(
                    authResult.UserName,
                    _tokenService.GenerateAccessToken(adminClaims));

            }

            return null!;


        }


    }
}
