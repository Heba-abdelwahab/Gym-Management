using AutoMapper;
using Domain.Constants;
using Domain.Contracts;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.ValueObjects;
using Services.Abstractions;
using Services.Specifications;
using Services.Specifications.CoachSpec;
using Shared;
using Shared.Auth;
using Shared.coach;
namespace Services
{
    public class CoachService : ICoachService
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userServices;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        private readonly ITokenService _tokenService;


        public CoachService(
            IAuthenticationService authenticationService,
            IUnitOfWork unitOfWork,
            IUserService userServices,
            IMapper mapper,
            IPhotoService photoService,
            ITokenService tokenService)
        {
            _authenticationService = authenticationService;
            _unitOfWork = unitOfWork;
            _userServices = userServices;
            _mapper = mapper;
            _photoService = photoService;
            _tokenService = tokenService;

        }

        public async Task<bool> RequestToBecomeCoachAsync(CoachRequestGymDto coachRequest)
        {
            var coachId = _userServices.Id;
            var gymRepo = await _unitOfWork.GetRepositories<Gym, int>().GetByIdAsync(coachRequest.gymId);
            if (gymRepo == null)
                return false;


            var request = new GymCoach
            {
                GymId = coachRequest.gymId,
                CoachId = coachId!.Value,
                WorkDays = _mapper.Map<ICollection<WorkDay>>(coachRequest.workDayDtos),
            };

            _unitOfWork.GetRepositories<GymCoach, int>().Insert(request);
            var result = await _unitOfWork.CompleteSaveAsync();
            return result;

        }

        public async Task<List<CoachReturnDto>> GetCoachesbyGym(int gymId)
        {
            var gym = await _unitOfWork.GetRepositories<Gym, int>().GetByIdAsync(gymId);
            if (gym is null)
            {
                throw new GymNotFoundException(gymId);
            }

            var coaches = await _unitOfWork.GetRepositories<Coach, int>()
                                           .GetAllWithSpecAsync(new GetCoaches(gymId));
            
            var result = _mapper.Map<List<CoachReturnDto>>(coaches, opt => opt.Items["gymid"]=gymId);

            return result;
        }

        #region Diet for Trainee
        public async Task<bool> CreateDietAsync(int traineeId, MealScheduleDto dietDto)
        {
            var coachId = _userServices.Id;

            var trainee = await _unitOfWork.GetRepositories<Trainee, int>().GetByIdAsync(traineeId);
            if (trainee is null)
            {
                throw new TraineeNotFoundException(traineeId);
            }

            var authorizedCoach = await IsCoachAuthorizedToAccessTraineeAsync(3, trainee);

            if (!authorizedCoach)
                throw new Exception("Un authorized Coach to access this trainee");

            var mealSchedule = _mapper.Map<MealSchedule>(dietDto);
            mealSchedule.CoachId = coachId!.Value;
            mealSchedule.TraineeId = traineeId;

            _unitOfWork.GetRepositories<MealSchedule, int>().Insert(mealSchedule);

            return await _unitOfWork.CompleteSaveAsync();
        }

        public async Task<MealScheduleResultDto?> GetDietByIdAsync(int dietId)
        {
            var diet = await _unitOfWork.GetRepositories<MealSchedule, int>().GetByIdWithSpecAsync(new GetDietByIdSpec(dietId));
            return diet is null ? null : _mapper.Map<MealScheduleResultDto>(diet);
        }

        public async Task<IEnumerable<MealScheduleResultDto>> GetDietsForTraineeAsync(int traineeId)
        {
            var trainee = await _unitOfWork.GetRepositories<Trainee, int>().GetByIdAsync(traineeId);
            if (trainee is null)
            {
                throw new TraineeNotFoundException(traineeId);
            }

            var diets = await _unitOfWork.GetRepositories<MealSchedule, int>().GetAllWithSpecAsync(new GetDietsForTraineeSpec(traineeId));
            return _mapper.Map<IEnumerable<MealScheduleResultDto>>(diets);
        }

        public async Task<bool> UpdateDietAsync(int dietId, MealScheduleUpdateDto dto)
        {
            var coachId = _userServices.Id;

            var dietToUpdate = await _unitOfWork.GetRepositories<MealSchedule, int>().GetByIdWithSpecAsync(new GetDietByIdSpec(dietId));

            if (dietToUpdate is null)
                throw new DietNotFoundException(dietId);

            if (dietToUpdate.CoachId != coachId!.Value)
                throw new Exception("Unauthorized: You are not the owner of this diet.");

            _mapper.Map(dto, dietToUpdate);
            _unitOfWork.GetRepositories<MealSchedule, int>().Update(dietToUpdate);
            return await _unitOfWork.CompleteSaveAsync();
        }

        public async Task<bool> DeleteDietAsync(int dietId)
        {
            var coachId = _userServices.Id;

            var dietToDelete = await _unitOfWork.GetRepositories<MealSchedule, int>().GetByIdAsync(dietId);
            if (dietToDelete is null)
            {
                throw new DietNotFoundException(dietId);
            }

            if (dietToDelete.CoachId != coachId!.Value)
            {
                throw new Exception("Unauthorized: You are not the owner of this diet.");
            }

            _unitOfWork.GetRepositories<MealSchedule, int>().Delete(dietToDelete);
            return await _unitOfWork.CompleteSaveAsync();
        }
        #endregion

        public async Task<AuthCoachResultDto> CreateCoachAsync(RegisterCoachDto request)
        {
            var registerUser = new RegisterUserDto
           (request.FirstName, request.LastName, request.UserName,
           request.Email, request.Password, request.PhoneNumber, Roles.Coach);


            var coach = new Coach();
            var photo = new Photo();
            #region upload CV & Image 
            var CvUploadedResult = await _photoService.UploadPdfAsync(request.CV);



            if (CvUploadedResult != null)
            {
                coach.CV = new MediaValueObj
                {
                    Url = CvUploadedResult.SecureUrl.AbsoluteUri,
                    PublicId = CvUploadedResult.PublicId,
                    Type = MediaType.PDF,
                };



            }

            var ImageUploadResult = await _photoService.AddPhotoFullPathAsync(request.Photo);

            if (ImageUploadResult is not null)
            {
                coach.Image = new MediaValueObj
                {
                    Url = ImageUploadResult.SecureUrl.AbsoluteUri,
                    PublicId = ImageUploadResult.PublicId,
                    Type = MediaType.Image,
                };

                photo = new Photo
                {
                    Url = ImageUploadResult.SecureUrl.AbsoluteUri,
                    PublicId = ImageUploadResult.PublicId,
                    IsMain = true
                };



            }



            #endregion


            var authResult = await _authenticationService.RegisterUserAsync(registerUser);


            coach.AppUserId = authResult.AppUserId;
            coach.Address = _mapper.Map<Address>(request.Address);
            coach.DateOfBirth = request?.DateOfBirth;
            coach.Specializations = request!.Specializations;
            photo.AppUserId = authResult.AppUserId;


            _unitOfWork.GetRepositories<Coach, int>().Insert(coach);
            _unitOfWork.GetRepositories<Photo, int>().Insert(photo);


            if (await _unitOfWork.CompleteSaveAsync())
            {

                var coachClaims = _tokenService.GenerateAuthClaims(
                        coach.Id, coach.AppUserId, registerUser.UserName,
                         registerUser.Email, registerUser.Role);

                return new AuthCoachResultDto(
                    authResult.UserName,
                    _tokenService.GenerateAccessToken(coachClaims));

            }

            return null!;

        }
        public async Task<IEnumerable<CoachPendingDto>> GetGymPendingCoachs(int gymId)
        {
            Gym? gym = await _unitOfWork.GetRepositories<Gym, int>().GetByIdAsync(gymId);
            if (gym == null)
                throw new GymNotFoundException(gymId);

            IEnumerable<GymCoach> gymCoachs = await _unitOfWork.GetRepositories<GymCoach, int>().GetAllWithSpecAsync(new GetGymCoachsByGymIdSpec(gymId));

            IEnumerable<CoachPendingDto> coachPendingDtos = _mapper.Map<IEnumerable<CoachPendingDto>>(gymCoachs);

            return coachPendingDtos;
        }

        public async Task HandleCoachJobRequest(int gymId, HandleJobRequestDto jobRequestDto)
        {
            var gymCoach = await _unitOfWork.GetRepositories<GymCoach, int>().GetByIdWithSpecAsync(new GetGymCoachSpec(gymId, jobRequestDto.CoachId));
            if (gymCoach == null)
                throw new GymCoachNotFoundException(gymId, jobRequestDto.CoachId);

            gymCoach.Status = jobRequestDto.IsAccepted == true ? RequestStatus.Accepted : RequestStatus.Rejected;

            if (!await _unitOfWork.CompleteSaveAsync())
                throw new Exception("fail to update the job request status");
        }

        #region Excercise Schdule for Trainee
        //CREATE
        public async Task<bool> CreateExerciseScheduleAsync(int traineeId, ExerciseScheduleDto exerciseScheduleDto)
        {
            var coachId = _userServices.Id;

            var trainee = await _unitOfWork.GetRepositories<Trainee, int>().GetByIdAsync(traineeId);
            if (trainee is null)
            {
                throw new TraineeNotFoundException(traineeId);
            }
            var authorizedCoach = await IsCoachAuthorizedToAccessTraineeAsync(coachId!.Value, trainee);

            if (!authorizedCoach)
                throw new Exception("Un authorized Coach to access this trainee");

            var exerciseSchedule = _mapper.Map<ExercisesSchedule>(exerciseScheduleDto);

            exerciseSchedule.CoachId = coachId!.Value;// coachId!.Value;
            exerciseSchedule.TraineeId = traineeId;

            _unitOfWork.GetRepositories<ExercisesSchedule, int>().Insert(exerciseSchedule);

            return await _unitOfWork.CompleteSaveAsync();
        }

        // --- READ ---
        public async Task<ExerciseScheduleResultDto?> GetExerciseScheduleByIdAsync(int scheduleId)
        {
            var schedule = await _unitOfWork.GetRepositories<ExercisesSchedule, int>().GetByIdWithSpecAsync(new GetScheduleByIdSpec(scheduleId));
            return schedule is null ? null : _mapper.Map<ExerciseScheduleResultDto>(schedule);
        }

        public async Task<IEnumerable<ExerciseScheduleResultDto>> GetExerciseSchedulesForTraineeAsync(int traineeId)
        {
            var trainee = await _unitOfWork.GetRepositories<Trainee, int>().GetByIdAsync(traineeId);
            if (trainee is null)
            {
                throw new TraineeNotFoundException(traineeId);
            }
            var schedules = await _unitOfWork.GetRepositories<ExercisesSchedule, int>().GetAllWithSpecAsync(new GetExerciseSchedulesForTraineeSpec(traineeId));
            return _mapper.Map<IEnumerable<ExerciseScheduleResultDto>>(schedules);
        }

        // --- UPDATE ---
        public async Task<bool> UpdateExerciseScheduleAsync(int scheduleId, ExerciseScheduleUpdateDto dto)
        {
            var coachId = _userServices.Id;

            var scheduleToUpdate = await _unitOfWork.GetRepositories<ExercisesSchedule, int>().GetByIdWithSpecAsync(new GetScheduleByIdSpec(scheduleId));
            if (scheduleToUpdate is null)
            {
                throw new ExerciseScheduleNotFoundException(scheduleId);
            }

            if (scheduleToUpdate.CoachId != coachId!.Value)
            {
                throw new Exception("Unauthorized: You are not the owner of this schedule.");
            }

            _mapper.Map(dto, scheduleToUpdate);

            _unitOfWork.GetRepositories<ExercisesSchedule, int>().Update(scheduleToUpdate);

            return await _unitOfWork.CompleteSaveAsync();
        }

        // --- DELETE ---
        public async Task<bool> DeleteExerciseScheduleAsync(int scheduleId)
        {
             var coachId = _userServices.Id;

            var scheduleToDelete = await _unitOfWork.GetRepositories<ExercisesSchedule, int>().GetByIdAsync(scheduleId);
            if (scheduleToDelete is null)
            {
                throw new ExerciseScheduleNotFoundException(scheduleId);
            }

            if (scheduleToDelete.CoachId != coachId!.Value)
            {
                throw new Exception("Unauthorized: You are not the owner of this schedule.");
            }

            _unitOfWork.GetRepositories<ExercisesSchedule, int>().Delete(scheduleToDelete);
            return await _unitOfWork.CompleteSaveAsync();
        }
        #endregion

        public async Task<bool> IsCoachAuthorizedToAccessTraineeAsync(int coachId, Trainee trainee)
        {

            if (trainee?.GymId is null)
                throw new Exception("Trainee has not joined any gym yet.");

            var coachGyms = await _unitOfWork.GetRepositories<GymCoach, int>().GetAllWithSpecAsync(new GymCoachesSpec(coachId));

            var coachGymIds = coachGyms.Select(gc => gc.GymId).ToList();

            if (!coachGymIds.Contains(trainee.GymId.Value))
                throw new Exception("The coach and the trainee are not in the same gym.");


            if (trainee?.CoachId is null)
                throw new Exception("Trainee has not been assigned to any coach.");

            return trainee.CoachId.Value == coachId;
        }

        public async Task<CoachDashboardToReturnDto> GetCoachDashboardAsync(int coachId)
        {

            var spec = new GetCoachDashboardSpec(coachId);
            var coach = (await _unitOfWork.GetRepositories<Coach, int>().GetByIdWithSpecAsync(spec));

            if (coach == null)
            {
                throw new CoeachesNotFoundException(coachId);
            }

            var result = _mapper.Map<CoachDashboardToReturnDto>(coach);

            return result;

        }

        public async Task<TraineeCoachDashboardDetailDto> GetTraineeDetailsForDashboardAsync(int traineeId)
        {
            var spec = new GetTraineeByIdSpec(traineeId);
            var trainee = await _unitOfWork.GetRepositories<Trainee, int>().GetByIdWithSpecAsync(spec);

            if (trainee == null)
            {
                throw new TraineeNotFoundException(traineeId); 
            }

            var traineeDto = _mapper.Map<TraineeCoachDashboardDetailDto>(trainee);

            return traineeDto;
        }

        public async Task<CoachInfoResultDto> GetCoachbyUserName(string username)
        {
            var coach = await _unitOfWork.GetRepositories<Coach, int>()
                .GetByIdWithSpecAsync(new GetCoachByAppUserIdSpec(_userServices.AppUserId!));



            return _mapper.Map<CoachInfoResultDto>(coach);

        }
    }
}
