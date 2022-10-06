using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Interfaces;
using WebApplication1.Tools.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WebApplication1.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly Context _context;
        public ChatRepository(Context context)
        {
            _context = context;
        }

        public void StoreMessage(ChatMessage message)
        {
            try
            {
                _context.ChatMessage.Add(message);
                _context.SaveChanges();
            }
            catch (System.Exception)
            {

                throw;
            }
            
        }
        public async Task<List<ChatMessage>> GetByGroup(int groupId)
        {
            var response = await _context.ChatMessage.Where(c => c.GroupId == groupId).ToListAsync();
            return response;
        }
    }
}
