using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities
{
    public class EventLog
    {
        public long Id { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Message { get; set; }

        public long? WeighingId { get; set; }

        public long? UserId { get; set; }

    }
}
