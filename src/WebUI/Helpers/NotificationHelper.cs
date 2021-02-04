using Application.Common.Enums;
using Application.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Helpers
{
    public static class NotificationHelper
    {

        public static void Alert(PageModel handler, string title, string message, NotificationType toastType = NotificationType.Success, NotificationPosition position = NotificationPosition.TopRight, int duration = 7500, bool showCloseButton = true, bool showProgressBar = true)
        {
            handler.TempData["AlertMessage"] = message;
            handler.TempData["AlertTitle"] = title;
            handler.TempData["AlertType"] = toastType.GetAttributeStringValue();
            handler.TempData["AlertDuration"] = duration;
            handler.TempData["AlertPosition"] = position.GetAttributeStringValue();
            handler.TempData["AlertShowCloseButton"] = showCloseButton;
            handler.TempData["AlertShowProgress"] = showProgressBar;
        }

        public static void Alert(Controller handler, string title, string message, NotificationType toastType = NotificationType.Success, NotificationPosition position = NotificationPosition.TopRight, int duration = 7500, bool showCloseButton = true, bool showProgressBar = true)
        {
            handler.TempData["AlertMessage"] = message;
            handler.TempData["AlertTitle"] = title;
            handler.TempData["AlertType"] = toastType.GetAttributeStringValue();
            handler.TempData["AlertDuration"] = duration;
            handler.TempData["AlertPosition"] = position.GetAttributeStringValue();
            handler.TempData["AlertShowCloseButton"] = showCloseButton;
            handler.TempData["AlertShowProgress"] = showProgressBar;
        }

        public static void Toast(Controller handler, string title, string message, NotificationType toastType, NotificationPosition position, int duration = 7500, bool showCloseButton = true, bool showProgressBar = true)
        {
            handler.TempData["ToastMessage"] = message;
            handler.TempData["ToastTitle"] = title;
            handler.TempData["ToastType"] = toastType.GetAttributeStringValue();
            handler.TempData["ToastDuration"] = duration;
            handler.TempData["ToastPosition"] = position.GetAttributeStringValue();
            handler.TempData["ToastShowCloseButton"] = showCloseButton;
            handler.TempData["ToastShowProgress"] = showProgressBar;
        }

        public static void Toast(PageModel handler, string title, string message, NotificationType toastType = NotificationType.Success, NotificationPosition position = NotificationPosition.TopRight, int duration = 7500, bool showCloseButton = true, bool showProgressBar = true)
        {
            handler.TempData["ToastMessage"] = message;
            handler.TempData["ToastTitle"] = title;
            handler.TempData["ToastType"] = toastType.GetAttributeStringValue();
            handler.TempData["ToastDuration"] = duration;
            handler.TempData["ToastPosition"] = position.GetAttributeStringValue();
            handler.TempData["ToastShowCloseButton"] = showCloseButton;
            handler.TempData["ToastShowProgress"] = showProgressBar;

        }
    }
}
