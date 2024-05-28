﻿namespace ElysionOrder.Domain.Entitys
{
    public class EBillSetting : BaseEntity
    {

        public string Role { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Reason { get; set; }
        public string ApplicationName { get; set; }
        public string HostName { get; set; }
        public string ChannelName { get; set; }
        public string Compressed { get; set; }
    }
}
