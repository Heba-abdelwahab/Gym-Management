using AutoMapper;
using Domain.Common;
using Domain.Contracts;
using Domain.Entities;
using Domain.Entities.Chat;
using Domain.ValueObjects.Chat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extensions;
using Presentation.Helper;
using Shared.Chat;

namespace Presentation.Controllers;

[Authorize]
public class MessagesController : ApiControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMessageRepository _messageRepository;
    private readonly IMapper _mapper;

    public MessagesController(UserManager<AppUser> userManager,
     IMessageRepository messageRepository, IMapper mapper)
    {
        _userManager = userManager;
        _messageRepository = messageRepository;
        _mapper = mapper;
    }


    [HttpPost]
    public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
    {
        var username = User.GetUserName();

        if (username == createMessageDto.RecipientUsername.ToLower())
            return BadRequest("you can't send message to yourself");

        var sender = await _userManager.FindByNameAsync(username);
        var recipent = await _userManager.FindByNameAsync(createMessageDto.RecipientUsername);

        if (recipent is null) return NotFound();


        var message = new Message
        {
            Sender = sender,
            Recipient = recipent,
            SenderUsername = sender.UserName,
            RecipientUsername = recipent.UserName,
            Content = createMessageDto.Content
        };

        _messageRepository.AddMessage(message);


        if (await _messageRepository.SaveAllAsync()) return Ok(_mapper.Map<MessageDto>(message));

        return BadRequest($"Falid to send message to {createMessageDto.RecipientUsername}");

    }

    [HttpGet]
    public async Task<ActionResult<PagedList<MessageDto>>> GetMessagesForUser([FromQuery] MessageParams messageParams)
    {

        messageParams.UserName = User.GetUserName();

        var messages = await _messageRepository.GetMessagesForUserAsync(messageParams);

        Response.AddPaginationHeader(
            new PaginationHeader(messages.CurrentPage, messages.PageSize, messages.TotalCount, messages.TotalPages));


        return Ok(messages);

    }


    [HttpGet("thread/{username}")]
    public async Task<ActionResult<IReadOnlyList<MessageDto>>> GetMessageThread(string username)
    {
        var currentUserName = User.GetUserName();

        return Ok(await _messageRepository.GetMessagesThreadAsync(currentUserName, username));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteMessage(int id)
    {
        var username = User.GetUserName();

        var message = await _messageRepository.GetMessage(id);

        if (message.SenderUsername != username && message.RecipientUsername != username)
            return Unauthorized();

        if (message.SenderUsername == username) message.SenderDeleted = true;
        if (message.RecipientUsername == username) message.RecipientDeleted = true;

        if (message.SenderDeleted && message.RecipientDeleted)
        {
            _messageRepository.DeleteMessage(message);
        }

        if (await _messageRepository.SaveAllAsync())
            return Ok();

        return BadRequest("Problem deleting the message.");
    }



}