using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Interfaces
{
    public interface IChatRepository
    {
        Task<List<ChatMessage>> GetByGroup(int groupId);
        void StoreMessage(ChatMessage message);
    }
}
