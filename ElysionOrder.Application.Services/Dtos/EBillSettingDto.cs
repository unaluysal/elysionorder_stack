using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElysionOrder.Application.Services.Dtos
{
    public class EBillSettingDto : BaseDto
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
