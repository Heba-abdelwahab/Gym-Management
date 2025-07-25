﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Common;
using Domain.Contracts;
using Domain.Entities.Chat;
using Domain.ValueObjects.Chat;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;


public class MessageRepository : IMessageRepository
{
    private readonly GymDbContext _dbContext;
    private readonly IMapper _mapper;

    public MessageRepository(GymDbContext context, IMapper mapper)
    {
        _dbContext = context;
        _mapper = mapper;
    }

    public void AddGroup(Group group)
        => _dbContext.Groups.Add(group);
    public async Task<Group?> GetMessageGroupAsync(string groupName)
        => await _dbContext.Groups.Include(g => g.Connections)
                                .FirstOrDefaultAsync(g => g.Name == groupName);
    public async Task<Connection?> GetConnectionAsync(string ConnectionId)
        => await _dbContext.Connections.FindAsync(ConnectionId);

    public async Task<Group?> GetGroupForConnectionAsync(string ConnectionId)
        => await _dbContext.Groups
            .Include(g => g.Connections)
            .Where(g => g.Connections.Any(c => c.ConnectionId == ConnectionId))
            .FirstOrDefaultAsync();
    public void RemoveConnection(Connection connection)
        => _dbContext.Connections.Remove(connection);
    public void AddMessage(Message message)
        => _dbContext.Messages.Add(message);
    public void DeleteMessage(Message message)
        => _dbContext.Messages.Remove(message);


    public async Task<Message?> GetMessage(int id)
        => await _dbContext.Messages.FindAsync(id);



    public async Task<PagedList<MessageDto>> GetMessagesForUserAsync(MessageParams messageParams)
    {
        var query = _dbContext.Messages.OrderByDescending(message => message.MessageSent).AsQueryable();

        query = messageParams.Container switch
        {
            "Inbox" => query.Where(message => message.RecipientUsername == messageParams.UserName
            && message.RecipientDeleted == false), //message from other user to me
            "Outbox" => query.Where(message => message.SenderUsername == messageParams.UserName
            && message.SenderDeleted == false), // message from me to other users 
            _ =>
                query.Where(message => message.RecipientUsername == messageParams.UserName
                && message.RecipientDeleted == false && message.DateRead == null) // unread message 
        };
        var messages = query.ProjectTo<MessageDto>(_mapper.ConfigurationProvider).AsNoTracking();

        return await PagedList<MessageDto>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);
    }

    public async Task<IReadOnlyList<MessageDto>> GetMessagesThreadAsync(string currentUserName, string recipientUserName)
    {
        var messages = await _dbContext.Messages
        .Include(u => u.Sender).ThenInclude(p => p.Photos)
        .Include(u => u.Recipient).ThenInclude(p => p.Photos)
        .Where(
             m =>
             (m.RecipientUsername == currentUserName &&
              m.SenderUsername == recipientUserName &&
               m.RecipientDeleted == false) ||
             (m.RecipientUsername == recipientUserName &&
              m.SenderUsername == currentUserName && m.SenderDeleted == false)

         ).OrderBy(m => m.MessageSent).ToListAsync(); //// from older to newer 
                                                      //  ).OrderByDescending(m => m.MessageSent).ToListAsync(); // from newer to older 


        //unread msg that i send and recive user still not read it 
        // var unreadMessages = messages.Where(m => m.DateRead is null && m.RecipientUsername == recipientUserName).ToList();

        //unread msg that any one send to me and i still not read it  

        var unreadMessages = messages
                                    .Where(m => m.DateRead is null && m.RecipientUsername == currentUserName)
                                    .ToList();


        if (unreadMessages.Any())
        {
            foreach (var msg in unreadMessages)
                msg.DateRead = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
        }

        return _mapper.Map<IReadOnlyList<MessageDto>>(messages);

    }



    public async Task<bool> SaveAllAsync()
        => await _dbContext.SaveChangesAsync() > 0;


}