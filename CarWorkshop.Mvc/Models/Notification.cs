namespace CarWorkshop.Mvc.Models;

public class Notification
{
    public NotificationTypeEnum Type { get; set; }
    public string Message { get; set; } = default!;

    public Notification(NotificationTypeEnum type, string message)
    {
        Type = type;
        Message = message;
    }
}
