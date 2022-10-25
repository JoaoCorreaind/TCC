using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Tools.DataBase;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace WebApplication1
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly Context _context;

        public NotificationRepository(Context context)
        {
            _context = context;
        }
        public async  Task<Notification> CreateNotification(NotificationDto notificationDto)
        {
            Notification notification = new Notification
            {
                CreatedAt = DateTime.Now,
                NotificationType = notificationDto.NotificationType,
                SenderUserId = notificationDto.SenderUserId,
                ReciverUserId = notificationDto.ReciverUserId,
            };
            if (!string.IsNullOrEmpty(notificationDto.GroupId.ToString()))
            {
                notification.GroupId = (int)notificationDto.GroupId;
            }
            if (!string.IsNullOrEmpty(notificationDto.SenderUserId))
            {
                notification.SenderUser = await _context.User.FindAsync(notificationDto.SenderUserId);
            }
            if (!string.IsNullOrEmpty(notificationDto.GroupId.ToString()))
            {
                notification.Group = await _context.Group.FindAsync(notificationDto.GroupId);
            }

            notification.ReciverUser = await _context.User.FindAsync(notificationDto.ReciverUserId);

            notification.Description = notificationDto.NotificationType == NotificationTypeEnum.Information ? notificationDto.Description : NotificationDescription(notification);

            _context.Notification.Add(notification);
            var result = await _context.SaveChangesAsync();
            if(result == 1)
            {
                return notification;
            }
            return null;
        }

        public Task<List<Notification>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<Notification> GetNotificationById(int notificationId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Notification>> GetUserNotifications(string userId)
        {
            try
            {
                var response =  await _context.Notification.Where(n => n.ReciverUserId == userId).Include(n => n.ReciverUser).Include(n=> n.Group!).Include(n=> n.SenderUser!).OrderByDescending(x=> x.CreatedAt).ToListAsync();
                return response;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<bool> ResolveInformationNotifications(string userId)
        {
            var notifications = await _context.Notification.Where(x => x.ReciverUserId == userId && x.NotificationType == NotificationTypeEnum.Information).ToListAsync();
            if(notifications.Count > 0)
            {
                notifications.ForEach(x => {
                    x.IsResolved = true;
                });
            }
           var response = await _context.SaveChangesAsync();
           if(response == notifications.Count)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UnresolvedNotification(NotificationDto notification)
        {
            var result = await _context.Notification.Where(x => !x.IsResolved && x.ReciverUserId == notification.ReciverUserId && x.SenderUserId == notification.SenderUserId || x.ReciverUserId == notification.SenderUserId && x.SenderUserId == notification.ReciverUserId && x.NotificationType == notification.NotificationType).ToListAsync();
            if (result != null && result.Count > 0)
            {
                if (notification.NotificationType == NotificationTypeEnum.GroupEntryRequest && result.Find(x=> x.GroupId == notification.GroupId)!= null) {
                    return true;
                }
                if (notification.NotificationType == NotificationTypeEnum.PrivateChatRequest && result.Find(x => x.ReciverUserId == notification.ReciverUserId) != null)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<Notification> UpdateNotification(NotificationDto notificationDto)
        {
            Notification notificationDb = await _context.Notification.Where(b => b.Id == notificationDto.Id).Include(n=> n.SenderUser).FirstOrDefaultAsync();
            notificationDb.IsResolved = notificationDto.IsResolved;
            notificationDb.ResolvedResult = notificationDto.ResolvedResult;

            try
            {
                await _context.SaveChangesAsync();
                return notificationDb;
            }
            catch (DbUpdateConcurrencyException ex)
            {

                throw ex;
            }
        }

        private string NotificationDescription(Notification not)
        {
            if(not.NotificationType == NotificationTypeEnum.GroupEntryRequest)
            {
                return $"Um usuário requisitou entrada no seu grupo {not.Group.Title}!";
            }
            if (not.NotificationType == NotificationTypeEnum.PrivateChatRequest)
            {
                return $"Um usuário requisitou iniciar um chat privado com você!";
            }
            if (not.NotificationType == NotificationTypeEnum.Information)
            {
                return $"Um usuário pediu para Entrar no seu grupo {not.Group.Title}!";
            }
            throw new("Tipo de notificação inválida");
        }
    }
}
