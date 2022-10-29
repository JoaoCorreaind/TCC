using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Interfaces;
using WebApplication1.Tools.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly Context _context;
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;

        public ChatRepository(Context context, IGroupRepository groupRepository, IUserRepository userRepository )
        {
            _context = context;
            _groupRepository = groupRepository;
            _userRepository = userRepository;
        }

        public void StoreMessage(ChatMessage message)
        {
            try
            {
                _context.ChatMessage.Add(message);
                _context.SaveChanges();
            }
            catch (System.Exception e)
            {

                throw e;
            }
            
        }
        public async Task<List<ChatMessage>> GetByGroup(int groupId)
        {
            var response = await _context.ChatMessage.Where(c => c.GroupId == groupId).ToListAsync();
            return response;
        }

        public async Task<List<ChatMessage>> GetByUser(string userId)
        {
            var response = await _context.ChatMessage.Where(c=> c.Group.Participants.Any(x=> x.Id == userId) || c.FriendShip.Users.Any(x=> x.Id == userId)).ToListAsync();
            return response;
        }

        public async Task<List<Chat>> GetChats(string userId)
        {
            try
            {
                List<Chat> chatList = new List<Chat>();
                var user = await _context.User.Where(x => x.Id == userId).Include(x => x.Groups).ThenInclude(g => g.GroupMainImage).Include(x => x.FriendShips).ThenInclude(f => f.Users).FirstOrDefaultAsync();
                foreach (var friend in user.FriendShips)
                {
                    User otherUser = friend.Users.Find(x => x.Id != userId);
                    Chat newChat = new Chat
                    {
                        Id = friend.Id,
                        Title = $"{otherUser.FirstName} {otherUser.LastName}",
                        ChatImage = otherUser.Image,
                        Type = MessageTypeEnum.PrivateChat

                    };
                    chatList.Add(newChat);
                }
                foreach (var group in user.Groups)
                {
                    Chat newChat = new Chat
                    {
                        Id = group.Id.ToString(),
                        Title = group.Title,
                        ChatImage = group.GroupMainImage?.Path,
                        Type = MessageTypeEnum.GroupChat
                    };
                    chatList.Add(newChat);
                }
                return chatList;
            }
            catch (System.Exception)
            {

                throw;
            }

        }
    }
}
