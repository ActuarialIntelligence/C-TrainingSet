using System;
using System.Collections.Generic;
using System.Text;

namespace AI.Domain.ContainerObjects
{
    public class Identifier
    {
        public string key { get; private set; }
        public Guid ID { get; private set; }
        public Identifier(string key, Guid ID)
        {
            this.key = key;
            this.ID = ID;
        }
    }
}
