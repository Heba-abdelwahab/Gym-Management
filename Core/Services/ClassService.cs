using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Services.Abstractions;
using Services.Specifications;
using Shared;

namespace Services
{
    internal class ClassService : IClassService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<Class, int> _classRepo;

        public ClassService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            _classRepo = unitOfWork.GetRepositories<Class, int>();
        }

        public async Task<IReadOnlyList<ClassToReturnDto>> GetAllClassesAsync()
        {
            var classes = await _classRepo.GetAllWithSpecAsync(new GetAllClassesSpec());
            var mappedClasses = _mapper.Map<IReadOnlyList<ClassToReturnDto>>(classes);
            
            return mappedClasses;
        }
        public async Task<IReadOnlyList<ClassToReturnDto>> GetClassesByGymAsync(int gymId)
        {
            var gym = await _unitOfWork.GetRepositories<Gym, int>().GetByIdAsync(gymId);
            if(gym is null)
            {
                throw new GymNotFoundException(gymId);
            }

            var gymClasses = await _classRepo.GetAllWithSpecAsync(new GetClassesByGymSpec(gymId));
            var mappedClasses = _mapper.Map<IReadOnlyList<ClassToReturnDto>>(gymClasses);

            return mappedClasses;
        }

        public async Task<ClassToReturnDto> GetClassByIdAsync(int id)
        {
            var selectedClass = await _classRepo.GetByIdWithSpecAsync(new GetClassByIdSpec(id));
            if(selectedClass is null)
            {
                throw new ClassNotFoundException(id);
            }

            var mappedClass = _mapper.Map<ClassToReturnDto>(selectedClass);
            return mappedClass;
        }

        public async Task<ClassToReturnDto> CreateClassAsync(ClassDto newClassDto)
        {
            var newClass = _mapper.Map<Class>(newClassDto);
            _classRepo.Insert(newClass);
            bool isAdded = await _unitOfWork.CompleteSaveAsync();

            if(isAdded)
            {
                var addedClass = await _classRepo.GetByIdWithSpecAsync(new GetClassByIdSpec(newClass.Id));
                return _mapper.Map<ClassToReturnDto>(addedClass);
            }

            throw new Exception("Failed to add the Entered Class.");
        }

        public async Task<ClassToReturnDto> UpdateClassAsync(int id, ClassDto updatedClassDto)
        {
            var mappedClass = _mapper.Map<Class>(updatedClassDto);
            mappedClass.Id = id;
            _classRepo.Update(mappedClass);
            bool isSaved = await _unitOfWork.CompleteSaveAsync();

            if (isSaved)
            {
                return _mapper.Map<ClassToReturnDto>(mappedClass);
            }

            throw new Exception($"Failed to update the Class with Id: {id}."); 
        }

        public async Task DeleteClassByIdAsync(int id)
        {
            var selectedClass = await _classRepo.GetByIdAsync(id);
            if (selectedClass is null)
            {
                throw new ClassNotFoundException(id);
            }

            _classRepo.Delete(selectedClass);
            bool isDeleted = await _unitOfWork.CompleteSaveAsync();

            if(!isDeleted)
            {
                throw new Exception($"Failed to delete the Class with Id: {id}");
            }

            /////// send notification to all trainees and coach
        }

        public async Task<IReadOnlyList<ClassTraineeToReturnDto>> GetClassTraineesAsync(int classId)
        {
            var selectedClass = await _classRepo.GetByIdWithSpecAsync(new GetClassTraineesSpec(classId));
            var mappedTrainees = _mapper.Map<IReadOnlyList<ClassTraineeToReturnDto>>(selectedClass.Trainees);

            return mappedTrainees;
        }

        public async Task RemoveTraineeFromClassAsync(int classId, int traineeId)
        {
            var selectedClass = await _classRepo.GetByIdWithSpecAsync(new GetClassTraineesSpec(classId));
            var selectedTrainee = await _unitOfWork.GetRepositories<Trainee, int>().GetByIdAsync(traineeId);

            if(selectedClass is null || selectedTrainee is null)
                throw new ClassNotFoundException(classId);

            selectedClass.Trainees?.Remove(selectedTrainee);
            bool isDeleted = await _unitOfWork.CompleteSaveAsync();

            if (!isDeleted)
                throw new Exception($"Failed to delete the Trainee with Id: {traineeId} from Class with Id: {classId}");

            /////// send notification to the trainee

        }

        public async Task<ClassTraineeToReturnDto> AddTraineeToClassAsync(int classId, int traineeId)
        {
            var selectedClass = await _classRepo.GetByIdWithSpecAsync(new GetClassTraineesSpec(classId));
            var selectedTrainee = await _unitOfWork.GetRepositories<Trainee, int>().GetByIdWithSpecAsync(new GetTraineeByIdSpec(traineeId));

            if (selectedClass is null || selectedTrainee is null)
                throw new ClassNotFoundException(classId);

            selectedClass.Trainees?.Add(selectedTrainee);
            bool isAdded = await _unitOfWork.CompleteSaveAsync();

            if(isAdded)
            {
                var mappedTrainee = _mapper.Map<ClassTraineeToReturnDto>(selectedTrainee);
                return mappedTrainee;
            }
            else
            {
                throw new Exception($"Failed to add the Trainee with Id: {traineeId} to Class with Id: {classId}.");
            }
        }

        public async Task<IReadOnlyList<ClassTraineeToReturnDto>> GetTraineesNotInClassAsync(int classId)
        {
            var selectedClass = await _classRepo.GetByIdWithSpecAsync(new GetClassTraineesSpec(classId));
            if (selectedClass is null)
            {
                throw new ClassNotFoundException(classId);
            }
               
            var notJoinedTrainees = await _unitOfWork.GetRepositories<Trainee, int>().GetAllWithSpecAsync(new GetTraineesNotInClass(classId));
            var mappedTrainees = _mapper.Map<IReadOnlyList<ClassTraineeToReturnDto>>(notJoinedTrainees);

            return mappedTrainees;
        }
    }
}
