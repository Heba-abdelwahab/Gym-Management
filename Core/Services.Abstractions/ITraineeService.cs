using Shared;

namespace Services.Abstractions;

public interface ITraineeService
{
    Task<AuthTraineeResultDto> CreateTraineeAsync(RegisterTraineeDto request);

}
