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

            throw new ClassNotFoundException(newClass.Id);
        }
    }
}
