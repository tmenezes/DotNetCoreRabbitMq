using System;

namespace DotNetCoreRabbitMq.Models
{
    public class Message
    {
        public Message(string source, string data)
        {
            Id = Guid.NewGuid().ToString();
            CreateDate = DateTime.Now;
            Source = source;
            Data = data;
        }

        public string Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Source { get; set; }
        public string Data { get; set; }
        public MessageType MessageType { get; set; }
    }

    public enum MessageType
    {
        Unknown = 0,
        Generic = 1,
        Specific = 2,
    }
}
