using Shared;

namespace Services.Abstractions
{
    public interface IClassService
    {
        Task<IReadOnlyList<ClassToReturnDto>> GetAllClassesAsync();
        Task<IReadOnlyList<ClassToReturnDto>> GetClassesByGymAsync(int gymId);
        Task<ClassToReturnDto> GetClassByIdAsync(int id);
        Task<ClassToReturnDto> CreateClassAsync(ClassDto newClassDto);
        Task<ClassToReturnDto> UpdateClassAsync(int id, ClassDto updatedClassDto);
        Task DeleteClassByIdAsync(int id);
        Task<IReadOnlyList<ClassTraineeToReturnDto>> GetClassTraineesAsync(int classId);
        Task RemoveTraineeFromClassAsync(int classId, int traineeId);
        Task<ClassTraineeToReturnDto> AddTraineeToClassAsync(int classId, int traineeId);
        Task<IReadOnlyList<ClassTraineeToReturnDto>> GetTraineesNotInClassAsync(int classId);
    }
}
