using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Web.SOAPServiceLibrary.DomainObjects
{
    public class ObjectStorePatternDominObject
    {
        private IDictionary<Identifier, string> rows { get; set; }
        public ObjectStorePatternDominObject()
        {

        }
        public ObjectStorePatternDominObject(IDictionary<Identifier, string> rows)
        {
            this.rows = rows;
        }

        public IList<string[]> Getwhere(Identifier ID, char delimiter)
        {
            var list = new List<string[]>();
            var result = rows.Where(r => r.Key.ID == ID.ID && r.Key.key == ID.key).Select(g => g.Value).ToList();
            foreach (var val in result)
            {
                list.Add(val.Split(delimiter));
            }
            return list;
        }
        public void Insert(Identifier ID, string data)
        {
            rows.Add(ID, data);
        }

        public bool IsIrregular(char delimiter)
        {
            var cnt = rows.First().Value.Split(delimiter).Count();

            foreach (var r in rows)
            {
                if (r.Value.Split(delimiter).Count() != cnt)
                {
                    return true;

                }
            }
            return false;

        }
    }

    internal class ObjectByteStorePatternDominObject
    {
        private IDictionary<Identifier, byte[]> rows { get; set; }
        public ObjectByteStorePatternDominObject()
        {
            rows = new Dictionary<Identifier, byte[]>();
        }
        public ObjectByteStorePatternDominObject(IDictionary<Identifier, byte[]> rows)
        {
            if (rows == null) 
            {
                rows = new Dictionary<Identifier, byte[]>(); 
            } 
            this.rows = rows;
        }
        public IList<byte[]> Getwhere(Identifier ID)
        {
            return rows.Where(s => s.Key.key == ID.key && s.Key.dateTime == ID.dateTime)
                .Select(p=>p.Value).ToList(); 
        }

        public void Insert(Identifier ID,byte[] data)
        {
            rows.Add(ID,data);
        }

    }
    public class ObjectStorePatternObject
    {
        public IDictionary<Identifier, string> rows { get; set; }
    }

    public class ObjectByteStorePatternObject
    {
        public IDictionary<Identifier, byte[]> bytes { get; set; }
    }
}
