using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Caching.Settings
{
    public class RedisSetting
    {
        public string RedisCacheUrl { get; set; }

        public int cacheExpirationTime { get; set; }

    }
}
