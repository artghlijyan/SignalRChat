﻿using System;

namespace SignalRChat.Models
{
    public class Message
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Text { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
