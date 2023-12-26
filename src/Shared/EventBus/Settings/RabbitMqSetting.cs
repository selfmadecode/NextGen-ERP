using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.EventBus
{
    public class RabbitMqSetting
    {
        public string Url { get; init; }
        public string Password { get; init; }
        public string Username { get; init; }
    }
}
