namespace Services.Abstractions;

public interface IServiceManager
{

    public IPhotoService PhotoService { get; }
    public IUserService UserServices { get; }
    public ITokenService TokenService { get; }
    public IAuthenticationService AuthenticationService { get; }
    public IAdminService AdminService { get; }
    public IGymOwnerService GymOwnerService { get; }
    public ICoachService CoachService { get; }
    public ITraineeService TraineeService { get; }
    public IClassService ClassService { get; }
    public IGymService GymService { get; }
}
