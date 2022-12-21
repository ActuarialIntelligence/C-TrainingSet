using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Web.SOAPServiceLibrary.DomainObjects
{
    public class Identifier
    {
        public DateTime dateTime { get; private set; }
        public string key { get; private set; }
        public Guid ID { get; private set; }
        public Identifier(string key, Guid ID, DateTime dateTime)
        {
            this.key = key;
            this.ID = ID;
            this.dateTime = dateTime;
        }
    }
}
