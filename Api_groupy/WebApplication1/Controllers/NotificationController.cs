using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Interfaces;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;


        public NotificationController(INotificationRepository notificationRepository, IGroupRepository groupRepository, IUserRepository userRepository)
        {
            _notificationRepository = notificationRepository;
            _groupRepository = groupRepository;
            _userRepository = userRepository;
        }

        // POST: api/notification
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Notification>>> CreateNotification([FromBody] NotificationDto notification)
        {
            try
            {
                var unresolvedNotification = await _notificationRepository.UnresolvedNotification(notification);

                if (unresolvedNotification)
                {
                    return BadRequest(new { message = "Já existe uma notificação pendente" });
                }

                return Ok(await _notificationRepository.CreateNotification(notification));

            }
            catch (Exception)
            {

                throw;
            }
        }
        // GET: api/notification
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetAll()
        {
            return await _notificationRepository.GetAll();
        }

        // GET: api/notification/id
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetById(int id)
        {
            return Ok(await _notificationRepository.GetNotificationById(id));
        }

        // GET: api/notification/user/id
        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetUserNotifications(string id)
        {
            return Ok(await _notificationRepository.GetUserNotifications(id));
        }

        // GET: api/notification/user/id
        [HttpPut("resolveNotification/{id}")]
        public async Task<ActionResult<IEnumerable<Notification>>> ResolveNotification(int id, [FromBody] NotificationDto notification)
        {
            if(id != notification.Id)
            {
                return BadRequest(); 
            }
            var notificationDb = await _notificationRepository.UpdateNotification(notification);
            if(notification.NotificationType == NotificationTypeEnum.GroupEntryRequest)
            {
                if (notificationDb.ResolvedResult == true)
                {
                    var result = await _groupRepository.AddParticipante(notification.SenderUserId, (int)notification.GroupId);
                    if (result.Participants.Find(x => x.Id == notification.SenderUserId) != null)
                    {
                        NotificationDto successNotification = new NotificationDto
                        {
                            Description = $"Seu ingresso no grupo {notificationDb.Group?.Title} foi aceito !!!",
                            NotificationType = NotificationTypeEnum.Information,
                            ReciverUserId = notification.SenderUserId,
                        };
                        await _notificationRepository.CreateNotification(successNotification);
                    }
                }
                else
                {
                    NotificationDto rejectNotification = new NotificationDto
                    {
                        Description = $"Seu ingresso no grupo {notificationDb.Group?.Title} foi negado pelo lider",
                        NotificationType = NotificationTypeEnum.Information,
                        ReciverUserId = notification.SenderUserId,
                    };
                    await _notificationRepository.CreateNotification(rejectNotification);
                }
            }
            if (notification.NotificationType == NotificationTypeEnum.PrivateChatRequest)
            {
                if (notificationDb.ResolvedResult == true)
                {
                    //var result = await _userRepository.CreateFriendRelationship(notification.SenderUserId, notification.ReciverUserId);
                    if(await _userRepository.CreateFriendRelationship(notification.SenderUserId, notification.ReciverUserId))
                    {
                        NotificationDto successNotification = new NotificationDto
                        {
                            Description = $"Seu convite foi aceito pelo usuário {notificationDb.ReciverUser.FirstName} {notificationDb.ReciverUser.LastName}!!!",
                            NotificationType = NotificationTypeEnum.Information,
                            ReciverUserId = notification.SenderUserId,
                        };
                        await _notificationRepository.CreateNotification(successNotification);
                    }                    
                }
                else
                {
                    NotificationDto rejectNotification = new NotificationDto
                    {
                        Description = $"Seu convite para chat privado foi negado pelo usuário {notificationDb.ReciverUser.FirstName} {notificationDb.ReciverUser.LastName} =(",
                        NotificationType = NotificationTypeEnum.Information,
                        ReciverUserId = notification.SenderUserId,
                    };
                    await _notificationRepository.CreateNotification(rejectNotification);
                }
            }
            return Ok();
        }

        // GET: api/notification/user/id
        [HttpPut("resolveInformationNotifications/{userId}")]
        public async Task<ActionResult<IEnumerable<Notification>>> ResolveInformationNotifications(string userId)
        {
           var response =  await _notificationRepository.ResolveInformationNotifications(userId);
            if (response)
            {
                return Ok();
            }
            return BadRequest();
        }

        //// PUT: api/notification
        //[HttpPut]
        //public async Task<ActionResult<IEnumerable<Notification>>> ReplyNotification([FromBody] Notification notification)
        //{
        //    throw 
        //}

    }
}
