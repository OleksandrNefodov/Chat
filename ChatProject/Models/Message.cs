using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatProject.Models
{
    public class Message
    {
        public Message()
        {
            Created = DateTime.UtcNow;
        }
        public Message(string text)
            : this()
        {
            Text = text;
        }

        public Message(string name, string text)
        : this()
        {
            UserName = name;
            Text = text;                  
        }
        public string Text { get; set; }

        public DateTime Created { get; set; }

        public string UserName { get; set; }
    }
}