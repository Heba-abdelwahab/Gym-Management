using AutoMapper;
using Domain.Common;
using Domain.Contracts;
using Domain.Entities;
using Domain.ValueObjects.member;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers;
using Presentation.Extensions;
using Presentation.Helper;
using Services.Abstractions;
namespace Gymawy.Controllers;


//public class UserController : ApiControllerBase
//{
//    private readonly IServiceManager _serviceManager;

//    public UserController(IServiceManager serviceManager)
//    {
//        _serviceManager = serviceManager;
//    }

//    [HttpGet]
//    public async Task<IActionResult> GetUser()
//    {
//        var users = await _serviceManager.UserServices.GetCairoUser();
//        return Ok(users);

//    }



//}

public record MemberUpdateDto();

[Authorize]
public class UsersController : ApiControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IServiceManager _serviceManager;
    public UsersController(IUserRepository userRepository,
        IMapper mapper, IServiceManager serviceManager)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<MemberDto>>> GetUsers([FromQuery] UserParams userParams)
    {

        var CurrentUser = await _userRepository.GetUserByUserNameAsync(User.GetUserName());
        userParams.UserName = CurrentUser?.UserName!;

        var users = await _userRepository.GerMembersAsync(userParams);

        Response.AddPaginationHeader(
            new PaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages));
        return Ok(users);
    }

    //[HttpGet("{id}")]
    //public async Task<ActionResult<MemberDto>> GetUser(string id)
    //    => Ok(_mapper.Map<MemberDto>(await _userRepository.GetUserByIdAsync(id)));

    [HttpGet("{username}")]
    public async Task<ActionResult<MemberDto>> GetUser(string username)
        => Ok(await _userRepository.GetMemberByUserNameAsync(username));

    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
    {

        var user = await _userRepository.GetUserByUserNameAsync(User.GetUserName());

        if (user is null) return NotFound("User not found");

        _mapper.Map(memberUpdateDto, user);

        if (await _userRepository.SaveAllAsync()) return NoContent();

        return BadRequest("Failed to update user");

    }


    [HttpPost("add-photo")]
    public async Task<ActionResult> AddPhoto(IFormFile file)
    {
        var user = await _userRepository.GetUserByUserNameAsync(User.GetUserName());

        if (user is null) return NotFound();

        var result = await _serviceManager.PhotoService.AddPhotoFullPathAsync(file);

        if (result is null) return BadRequest();

        var photo = new Photo
        {
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId
        };

        if (user.Photos.Count == 0)
            photo.IsMain = true;

        user.Photos.Add(photo);

        if (await _userRepository.SaveAllAsync())
            return CreatedAtAction(
                actionName: nameof(GetUser),
                routeValues: new { username = user.UserName },
                value: _mapper.Map<PhotoDto>(photo)
            );

        return BadRequest("Problem , Can't add photo pls try again later.");


    }

    [HttpPut("set-main-photo/{photoId}")]
    public async Task<ActionResult> SetMainPhoto(int photoId)
    {

        var user = await _userRepository.GetUserByUserNameAsync(User.GetUserName());

        if (user is null) return NotFound();

        var oldMainPhoto = user.Photos.FirstOrDefault(photo => photo.IsMain);
        if (oldMainPhoto is not null) oldMainPhoto.IsMain = false;

        var photo = user.Photos.FirstOrDefault(photo => photo.Id == photoId);

        if (photo is null) return NotFound();
        if (photo.IsMain) return BadRequest("this is already your main photo .");

        photo.IsMain = true;

        if (await _userRepository.SaveAllAsync()) return NoContent();

        return BadRequest("Problem setting the main photo ");

    }



    [HttpDelete("delete-photo/{photoId}")]
    public async Task<ActionResult> RemovePhoto(int photoId)
    {
        var user = await _userRepository.GetUserByUserNameAsync(User.GetUserName());
        if (user is null) return NotFound();

        var photo = user.Photos.FirstOrDefault(photo => photo.Id == photoId);

        if (photo is null) return NotFound();
        if (photo.IsMain) return BadRequest("Can't delete  main photo .");




        if (photo.PublicId is not null && await _serviceManager.PhotoService.DeletePhotoAsync(photo.PublicId))
            user.Photos.Remove(photo);

        if (await _userRepository.SaveAllAsync())
            return Ok();

        return BadRequest("Problem setting the main photo ");

    }

}
