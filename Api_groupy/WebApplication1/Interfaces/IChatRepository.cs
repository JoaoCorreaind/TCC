using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IChatRepository
    {
        Task<List<ChatMessage>> GetByGroup(int groupId);

        Task<List<ChatMessage>> GetByUser(string groupId);
        Task<List<Chat>> GetChats(string groupId);
        void StoreMessage(ChatMessage message);
    }
}
