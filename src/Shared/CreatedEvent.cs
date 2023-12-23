using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
   public record CreatedEvent
    {
        public Guid Id { get; set; }
        public DateTime CreatedOnUtc { get; set; }
    }

    public record ViewedEvent
    {
        public Guid Id { get; set; }
        public DateTime ViewedOnUtc { get; set; }
    }
}
