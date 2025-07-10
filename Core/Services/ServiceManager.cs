using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Services.Abstractions;
using Shared.Cloudinary;
using Shared.Jwt;

namespace Services;

public class ServiceManager : IServiceManager
{

    private readonly Lazy<IPhotoService> _lazyPhotoService;
    private readonly Lazy<IUserService> _lazyUserService;
    private readonly Lazy<ITokenService> _lazyTokenService;
    private readonly Lazy<IAuthenticationService> _lazyAuthenticationService;
    private readonly Lazy<IAdminService> _lazyAdminService;
    private readonly Lazy<IClassService> _lazyClassService;
    private readonly Lazy<ICoachService> _lazyCoachService;
    private readonly Lazy<ITraineeService> _lazyTraineeService;
    private readonly Lazy<IGymService> _lazyGymService;
    private readonly Lazy<IGymOwnerService> _lazyGymOwnerService;


    public ServiceManager(
        IOptionsMonitor<CloudinarySettings> config,
        IOptionsMonitor<JwtOptions> jwtOptions,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        UserManager<AppUser> userManager,
        IMapper mapper)
    {

        _lazyPhotoService = new(() => new PhotoService(config));
        _lazyUserService = new(() => new UserService(httpContextAccessor, unitOfWork));
        _lazyTokenService = new(() => new TokenService(jwtOptions));
        _lazyAuthenticationService = new(() => new AuthenticationService(userManager, TokenService, unitOfWork));
        _lazyAdminService = new(() => new AdminService(AuthenticationService, unitOfWork, TokenService));
        _lazyClassService = new(() => new ClassService(unitOfWork, mapper));
        _lazyGymOwnerService = new(() => new GymOwnerService(unitOfWork, mapper));
        _lazyCoachService = new(() => new CoachService(AuthenticationService, unitOfWork, UserServices, mapper, TokenService));
        _lazyTraineeService = new(() => new TraineeService(AuthenticationService, unitOfWork, UserServices, mapper, TokenService));
        _lazyGymService = new(() => new GymService(unitOfWork, mapper,UserServices,PhotoService, config));
    }

    public IPhotoService PhotoService => _lazyPhotoService.Value;

    public IUserService UserServices => _lazyUserService.Value;

    public ITokenService TokenService => _lazyTokenService.Value;

    public IAuthenticationService AuthenticationService => _lazyAuthenticationService.Value;

    public IAdminService AdminService => _lazyAdminService.Value;

    public ICoachService CoachService => _lazyCoachService.Value;

    public ITraineeService TraineeService => _lazyTraineeService.Value;
    public IClassService ClassService => _lazyClassService.Value;
    public IGymOwnerService GymOwnerService => _lazyGymOwnerService.Value;

    public IGymService GymService => _lazyGymService.Value;
}
