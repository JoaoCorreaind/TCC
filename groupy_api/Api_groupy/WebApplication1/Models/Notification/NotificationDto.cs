namespace WebApplication1.Models
{
    public class NotificationDto
    {
        public int? Id { get; set; }
        public NotificationTypeEnum NotificationType { get; set; }
        public int? GroupId { get; set; } //when NotificationType == NotificationTypeEnum.GroupEntryRequest
        public string Description { get; set; }
        public string SenderUserId { get; set; } //when NotificationType == NotificationTypeEnum.GroupEntryRequest or NotificationTypeEnum.PrivateChatRequest
        public string ReciverUserId { get; set; }
        public bool IsResolved { get; set; } = false;
        public bool ResolvedResult { get; set; } = false;

    }
}
