using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChatProject.Models;

namespace ChatProject.Controllers
{
    public class HomeController : Controller
    {
        public static ChatModel chat;

        // GET: Home
        public ActionResult Index(string name, bool? logOn, bool? logOff, string message)
        {
            if (chat == null)
            {
                chat = new ChatModel();
            }

            if (!Request.IsAjaxRequest())
            {
                return View(chat);
            }

            var msgCount = chat.Messages.Count;

            //Always show only 10 last messages
            if (msgCount > 10)
            {
                var dif = msgCount - 10;
                chat.Messages.RemoveRange(0, dif);
            }

            if (logOn != null && (bool)logOn)
            {
                var user = chat.Users.FirstOrDefault(item => item.Name == name);
                if (user == null)
                {
                    chat.Users.Add(new User
                    {
                        Name = name,
                        LoginDateTime = DateTime.UtcNow
                    });
                    chat.Messages.Add(new Message($"User {name} has joined chat."));

                    return PartialView("ChatRoom", chat);
                }

                return new HttpStatusCodeResult(400, "User with same name already exists in the chat."); 
            }
            else if (logOff != null && (bool)logOff)
            {
                var user = chat.Users.FirstOrDefault(item => item.Name == name);
                LogOff(user);     
                           
                return PartialView("ChatRoom", chat);
            }
            else
            {
                var currentUser = chat.Users.First(user => user.Name == name);
                currentUser.LoginDateTime = DateTime.UtcNow;

                //remove all exired users
                var users = chat.Users.Where(user => (DateTime.UtcNow - user.LoginDateTime).Minutes > 1).ToList();
                foreach (var chatUser in users)
                {
                    LogOff(chatUser, $"User {chatUser.Name} was kicked from chat due to timeout.");
                }

                if (!string.IsNullOrEmpty(message))
                {
                    var msg = new Message(name, message);
                    chat.Messages.Add(msg);                  
                }

                return PartialView("History", chat);
            }
        }

        public void LogOff(User user, string msg = "")
        {
            if (user != null)
            {
                chat.Users.Remove(user);
                string message = !string.IsNullOrEmpty(msg)
                    ? msg
                    : $"User {user.Name} has left chat.";

                chat.Messages.Add(new Message(message));
            }
        }
    }
}