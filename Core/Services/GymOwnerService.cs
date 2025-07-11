using AutoMapper;
using Domain.Constants;
using Domain.Contracts;
using Domain.Entities;
using Services.Abstractions;
using Services.Specifications.GymOwnerSpec;
using Shared.Auth;
using Shared.GymOwner;

namespace Services;

internal class GymOwnerService : IGymOwnerService
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public GymOwnerService(IAuthenticationService authenticationService,
        IUnitOfWork unitOfWork,
        ITokenService tokenService,
        IMapper mapper,
        IUserService userService
        )
    {
        _authenticationService = authenticationService;
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _mapper = mapper;
        _userService = userService;
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

    public async Task<GymOwnerInfoResultDto> GetGymOwnerByUserNameAsync(string username)
    {
        var gymOwner = await _unitOfWork.GetRepositories<GymOwner, int>()
                 .GetByIdWithSpecAsync(new GetGymOwnerByAppUserIdSpec(_userService.AppUserId!));



        return _mapper.Map<GymOwnerInfoResultDto>(gymOwner);
    }
}
