using Domain.Constants;
using Domain.Contracts;
using Domain.Entities;
using Services.Abstractions;
using Shared;

namespace Services;

//public record AmindRegisterDto(string UserName, string Password, string Role);
//record UserRegisterDto(string UserName, string Password, string Role);

//public class AdminService
//{
//    private readonly AuthenticationService authenticationService;

//    public AdminService(IUnitOfWork unitOfWork, AuthenticationService authenticationService)
//    {
//        UnitOfWork = unitOfWork;
//        this.authenticationService = authenticationService;
//    }

//    private IUnitOfWork UnitOfWork { get; }

//    public async void CreateAdmin(AmindRegisterDto dto)
//    {
//        //validation by usermanager like unique username
//        AppUser appUser = new AppUser()
//        {
//            UserName = dto.UserName,

//        };
//        bool isSuccess = await authenticationService.Register(appUser, dto.Password, Roles.Admin);
//        if (isSuccess)
//        {
//            Admin admin = new Admin()
//            {
//                AppUser = appUser,
//                Hamada = "Ddd",
//                AppUserId = appUser.Id
//            };
//            UnitOfWork.GetRepositories<Admin, int>().Insert(admin);
//        }

//    }
//}

//public class AuthenticationService
//{
//    private readonly UserManager<AppUser> userManager;

//    public AuthenticationService(UserManager<AppUser> userManager)
//    {
//        this.userManager = userManager;
//    }

//    public async Task<bool> Register(AppUser appUser , string password,string role)
//    {
//        var res= await userManager.CreateAsync(appUser , password);
//        if (res.Succeeded)
//        {
//            await userManager.AddToRoleAsync(appUser, role);
//            return true;
//        }
//        else
//        {
//            string errors = string.Join(", ",res.Errors.Select(e=>e.Description));
//            return false;
//        }
//    }

//}


public class AdminService : IAdminService
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;

    public AdminService(IAuthenticationService authenticationService,
        IUnitOfWork unitOfWork,
        ITokenService tokenService
        )
    {
        _authenticationService = authenticationService;
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;

    }




    public async Task<AuthAdminResultDto> CreateAdminAsync(RegisterUserDto request)
    {

        var registerUser = new RegisterUserDto // need to change later
            (request.FirstName, request.LastName, request.UserName,
            request.Email, request.Password, request.PhoneNumber, Roles.Admin);

        var authResult = await _authenticationService.RegisterUserAsync(registerUser);


        var admin = new Admin
        {
            AppUserId = authResult.AppUserId,
            Hamada = "test ya hamada"
        };

        _unitOfWork.GetRepositories<Admin, int>().Insert(admin);


        if (await _unitOfWork.CompleteSaveAsync())
        {

            var adminClaims = _tokenService.GenerateAuthClaims(
                    admin.Id, admin.AppUserId, registerUser.UserName,
                     registerUser.Email, registerUser.Role);

            return new AuthAdminResultDto(
                authResult.UserName,
                _tokenService.GenerateAccessToken(adminClaims));

        }

        return null!;


    }
}
