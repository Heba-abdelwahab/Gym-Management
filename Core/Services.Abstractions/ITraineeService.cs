using Shared;

namespace Services.Abstractions;

public interface ITraineeService
{
    List<TraineeToReturnDto> GetTraineebyGym(int gymId);

}
