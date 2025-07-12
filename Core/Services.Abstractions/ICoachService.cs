using Domain.Entities;
using Shared;
using Shared.Auth;
using Shared.coach;

namespace Services.Abstractions;

public interface ICoachService
{
    // Request into gym to become a coach
    Task<bool> RequestToBecomeCoachAsync(CoachRequestGymDto coachRequest);
    Task<List<CoachReturnDto>> GetCoachesbyGym(int gymId);
    Task<CoachInfoResultDto> GetCoachbyUserName(string username);

    #region Diet for trainee
    Task<bool> CreateDietAsync(int traineeId, MealScheduleDto dietDto);
    Task<MealScheduleResultDto?> GetDietByIdAsync(int dietId);
    Task<MealScheduleResultDto> GetDietForTraineeAsync(int traineeId);
    Task<bool> UpdateDietAsync(int dietId, MealScheduleUpdateDto dto);
    Task<bool> DeleteDietAsync(int dietId);
    #endregion
    Task<AuthCoachResultDto> CreateCoachAsync(RegisterCoachDto request);



    Task<IEnumerable<CoachPendingDto>> GetGymPendingCoachs(int gymId);
    Task HandleCoachJobRequest(int gymId, HandleJobRequestDto jobRequestDto);

    #region Excercise Schdule for Trainee
    // CREATE
    Task<bool> CreateExerciseScheduleAsync(int traineeId, ExerciseScheduleDto exerciseScheduleDto);

    // READ
    Task<ExerciseScheduleResultDto?> GetExerciseScheduleByIdAsync(int scheduleId);
    Task<ExerciseScheduleResultDto> GetExerciseSchedulesForTraineeAsync(int traineeId);

    // UPDATE
    Task<bool> UpdateExerciseScheduleAsync(int scheduleId, ExerciseScheduleUpdateDto dto);

    // DELETE
    Task<bool> DeleteExerciseScheduleAsync(int scheduleId);

    // AUTHORIZATION
    Task<bool> IsCoachAuthorizedToAccessTraineeAsync(int coachId, Trainee trainee);
    #endregion

    Task<CoachDashboardToReturnDto> GetCoachDashboardAsync(int coachId);
    Task<TraineeCoachDashboardDetailDto> GetTraineeDetailsForDashboardAsync(int traineeId);

    Task<IEnumerable<MuscleDto>> GetAllMusclesWithExercisesAsync();
}
