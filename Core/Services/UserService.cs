using Shared;
using Domain.Entities;
using Domain.Contracts;
using Services.Abstractions;
using Services.Specifications; 
namespace Services
{
    public class UserService:IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task< IEnumerable< UserDto> >GetCairoUser()
        {
           IRepository<User,int> repo=  unitOfWork.GetRepositories<User, int>();
            var users= await repo.GetAllWithSpecAsync(new GetCairoUsersSpec());
            List<UserDto> userDtos = new List<UserDto>();
            foreach (var user in users) {
                UserDto userDto = new UserDto()
                {
                    Address = user.address,
                    Id = user.Id,
                    Name = user.name
                };
                userDtos.Add(userDto);
            }
            return userDtos;
        }
    }
}
