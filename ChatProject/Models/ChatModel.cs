using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatProject.Models
{
    public class ChatModel
    {
        public ChatModel()
        {
            Users = new List<User>();
            Messages = new List<Message>();

            Messages.Add(new Message
            {
                Text = "Chat started at " + DateTime.UtcNow
            });
        }

        public List<User> Users { get; set; }
        public List<Message> Messages { get; set; }
    }
}