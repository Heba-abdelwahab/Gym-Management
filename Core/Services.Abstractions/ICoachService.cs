using Domain.Entities;
using Shared;

namespace Services.Abstractions;

public interface ICoachService
{
    // Request into gym to become a coach
    Task<bool> RequestToBecomeCoachAsync(CoachRequestGymDto coachRequest);
    Task<List<CoachToReturnDto>> GetCoachesbyGym(int gymId);

    // Creates a new meal schedule for a trainee.
    Task<bool> CreateDietAsync(int traineeId, MealScheduleDto dietDto);


    Task<AuthCoachResultDto> CreateCoachAsync(RegisterCoachDto request);



    Task<IEnumerable<CoachPendingDto>> GetGymPendingCoachs(int gymId);
    Task HandleCoachJobRequest(int gymId, HandleJobRequestDto jobRequestDto);

    #region Excercise Schdule for Trainee
    // CREATE
    Task<bool> CreateExerciseScheduleAsync(int traineeId, ExerciseScheduleDto exerciseScheduleDto);

    // READ
    Task<ExerciseScheduleResultDto?> GetExerciseScheduleByIdAsync(int scheduleId);
    Task<IEnumerable<ExerciseScheduleResultDto>> GetExerciseSchedulesForTraineeAsync(int traineeId);

    // UPDATE
    Task<bool> UpdateExerciseScheduleAsync(int scheduleId, ExerciseScheduleUpdateDto dto);

    // DELETE
    Task<bool> DeleteExerciseScheduleAsync(int scheduleId);

    // AUTHORIZATION
    Task<bool> IsCoachAuthorizedToAccessTraineeAsync(int coachId, Trainee trainee);
    #endregion
}
