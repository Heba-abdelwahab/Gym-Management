using Shared;

namespace Services.Abstractions
{
    public interface IClassService
    {
        Task<IReadOnlyList<ClassToReturnDto>> GetAllClassesAsync();
        Task<ClassToReturnDto> GetClassByIdAsync(int id);
        Task<ClassToReturnDto> CreateClassAsync(ClassDto newClassDto);
        Task<ClassToReturnDto> UpdateClassAsync(int id, ClassDto updatedClassDto);
        Task DeleteClassByIdAsync(int id);
    }
}
