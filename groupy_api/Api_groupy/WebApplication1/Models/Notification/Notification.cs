using System;

namespace WebApplication1.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public NotificationTypeEnum NotificationType { get; set; }
        public string Description { get; set; }
        public int? GroupId { get; set; } //when NotificationType == NotificationTypeEnum.GroupEntryRequest
        public Group? Group { get; set; } = null;//when NotificationType == NotificationTypeEnum.GroupEntryRequest
        public string? SenderUserId { get; set; } //when NotificationType == NotificationTypeEnum.GroupEntryRequest or NotificationTypeEnum.PrivateChatRequest
        public User? SenderUser { get; set; } = null;
        public string ReciverUserId { get; set; }
        public User ReciverUser { get; set; }
        public bool IsResolved { get; set; }
        public bool ResolvedResult { get; set; }
        public DateTime CreatedAt { get; set; }

    }

}
