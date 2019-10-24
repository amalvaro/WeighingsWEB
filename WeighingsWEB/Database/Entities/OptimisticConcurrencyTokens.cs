using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities
{
    public class OptimisticConcurrencyTokens
    {
        public string Name { get; set; }

        public Guid Value { get; set; }

    }

}
