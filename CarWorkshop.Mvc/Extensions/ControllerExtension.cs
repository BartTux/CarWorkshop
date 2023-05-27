using CarWorkshop.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarWorkshop.Mvc.Extensions;

public static class ControllerExtension
{
    public static void SetNotification(this Controller controller,
                                       NotificationTypeEnum type,
                                       string message) 
        => controller.TempData["Notification"] 
            = JsonConvert.SerializeObject(new Notification(type, message));
}
