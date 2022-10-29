using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatRepository _chatRepository;
        public ChatController(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        [HttpGet("{groupId}/messages")]
        public async Task<ActionResult<
            ChatMessage>> GetByUser(int groupId)
        {
            var response = await _chatRepository.GetByGroup(groupId);
            
            return Ok(response);
        }

        [HttpGet("{userId}/user-messages")]
        public async Task<ActionResult<ChatMessage>> GetGroupsMessage(string userId)
        {
            var response = await _chatRepository.GetByUser(userId);

            return Ok(response);
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<ChatMessage>> GetChats(string userId)
        {
            var response = await _chatRepository.GetChats(userId);

            return Ok(response);
        }
    }
}
