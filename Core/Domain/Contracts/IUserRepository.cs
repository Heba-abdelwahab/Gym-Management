using Domain.Common;
using Domain.Entities;
using Domain.ValueObjects.member;

namespace Domain.Contracts;

public interface IUserRepository
{
    void Update(AppUser user);
    Task<bool> SaveAllAsync();

    Task<IReadOnlyList<AppUser>> GerUsersAsync();
    Task<AppUser?> GetUserByIdAsync(string id);
    Task<AppUser?> GetUserByUserNameAsync(string userName);

    Task<PagedList<MemberDto>> GerMembersAsync(UserParams userParams);
    Task<MemberDto?> GetMemberByUserNameAsync(string userName);
}
