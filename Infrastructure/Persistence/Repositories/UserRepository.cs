using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Common;
using Domain.Contracts;
using Domain.Entities;
using Domain.ValueObjects.member;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly GymDbContext _dbContext;
    private readonly IMapper _mapper;
    //private readonly UserManager<AppUser> _userManager;

    public UserRepository(GymDbContext context, IMapper mapper
        /*UserManager<AppUser> userManager*/)
    {
        _dbContext = context;
        _mapper = mapper;
        //_userManager = userManager;
    }

    public async Task<PagedList<MemberDto>> GerMembersAsync(UserParams userParams)
    {
        // var query = _context.Users.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).AsNoTracking();

        var query = _dbContext.Users.AsQueryable();
        query = query.Where(user => user.UserName != userParams.UserName);

        //query = userParams.OrderBy switch
        //{
        //    "created" => query.OrderByDescending(user => user.Created),
        //    _ => query.OrderByDescending(user => user.LastActive)
        //};


        return await PagedList<MemberDto>.CreateAsync(
            query.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).AsNoTracking(),
                userParams.PageNumber, userParams.PageSize);

    }

    public async Task<MemberDto?> GetMemberByUserNameAsync(string userName)
        => await _dbContext.Users.ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                                .SingleOrDefaultAsync(user => user.UserName == userName);
    public async Task<IReadOnlyList<AppUser>> GerUsersAsync()
        => await _dbContext.Users
                        .Include(user => user.Photos)
                        .ToListAsync();


    public async Task<AppUser?> GetUserByIdAsync(string id)
        => await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);

    public async Task<AppUser?> GetUserByUserNameAsync(string userName)
        => await _dbContext.Users
                        .Include(user => user.Photos)
                        .SingleOrDefaultAsync(user => user.UserName == userName);

    public async Task<bool> SaveAllAsync()
        => await _dbContext.SaveChangesAsync() > 0;

    public void Update(AppUser user)
        => _dbContext.Users.Update(user);
}