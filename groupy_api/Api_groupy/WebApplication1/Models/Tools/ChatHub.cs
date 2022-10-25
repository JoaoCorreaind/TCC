using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Tools.DataBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using WebApplication1.Interfaces;
using WebApplication1.Tools;
using WebApplication1.Models;

namespace WebApplication1.Models.Tools
{
    [Authorize]
    [AllowAnonymous]
    public class ChatHub : Hub
    {
        private readonly Context _context;
        private readonly IHttpContextAccessor _acessor;
        private readonly IChatRepository _chatRepository;
        public ChatHub(Context context, IHttpContextAccessor accessor, IChatRepository chatRepository)
        {
            _context = context;
            _acessor = accessor;
            _chatRepository = chatRepository;
        }

        public string GetConnectionId() => Context.ConnectionId;

        public override async Task<Task> OnConnectedAsync()
        {

            try
            {
             
                //var userId = _acessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("id", StringComparison.OrdinalIgnoreCase))?.Value;
                var userId = Functions.GetStringFromUrl( _acessor.HttpContext.Request.QueryString.ToString(), "userId");

                var user = await _context.User.Where(u => u.Id == userId).Include(u => u.Groups).FirstOrDefaultAsync();
                
                if (user.Groups != null || user.Groups.Count > 0)
                {
                    // Add to each assigned group.
                    foreach (var item in user.Groups)
                    {
                        await Groups.AddToGroupAsync(Context.ConnectionId, item.Id.ToString());
                    }
                }

                return base.OnConnectedAsync();
            }
            catch (Exception e)
            {

                throw e;
            }
           
        }

        public async Task BroadcastToGroup(string groupName) => await Clients.Group(groupName)
        .SendAsync("broadcasttogroup", $"{Context.ConnectionId} has joined the group {groupName}.");

        public async void Send(int groupId, User user, string message)
        {
            try
            {
                ChatMessage messageStore = new ChatMessage
                {
                    GroupId = groupId,
                    SenderId = user.Id,
                    SenderName = user.FirstName + " " + user.LastName,
                    SentAt = DateTime.Now,
                    Text = message,
                    SenderImagePath = user.Image,
                };

                await Clients.Group(groupId.ToString()).SendAsync("send", user, messageStore);
                
                _chatRepository.StoreMessage(messageStore);

            }
            catch (Exception e)
            {

                throw;
            }

        }
        public void AddToRoom(string roomName)
        {

            // Retrieve room.
            var room = _context.Group.Find(roomName);

            if (room != null)
            {
                Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            }

        }

        public void RemoveFromRoom(string roomName)
        {

            // Retrieve room.
            var room = _context.Group.Find(roomName);
            if (room != null)
            {
                Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
            }
        }
    }
}


public class ChatMessage
{
    public int Id { get; set; }
    public string SenderName { get; set; }
    public string SenderId { get; set; }
    public string SenderImagePath { get; set; }
    public string Text { get; set; }
    public bool IsPictureMessage { get; set; } = false;
    public int GroupId { get; set; }
    public Group Group { get; set; }
    public DateTimeOffset SentAt { get; set; }
}

public class ChatRoom
{
    public string OwnerConnectionId { get; set; }
}

public interface IChatRoomService
{
    Task<Guid> CreateRoom(string connectionId);

    Task<Guid> GetRoomForConnectionId(string connectionId);
}

public class InMemoryChatRoomService : IChatRoomService
{
    private readonly Dictionary<Guid, ChatRoom> _roomInfo
        = new Dictionary<Guid, ChatRoom>();

    public Task<Guid> CreateRoom(string connectionId)
    {
        var id = Guid.NewGuid();
        _roomInfo[id] = new ChatRoom
        {
            OwnerConnectionId = connectionId
        };

        return Task.FromResult(id);
    }

    public Task<Guid> GetRoomForConnectionId(string connectionId)
    {
        var foundRoom = _roomInfo.FirstOrDefault(
            x => x.Value.OwnerConnectionId == connectionId);

        if (foundRoom.Key == Guid.Empty)
            throw new ArgumentException("Invalid connection ID");

        return Task.FromResult(foundRoom.Key);
    }

}
