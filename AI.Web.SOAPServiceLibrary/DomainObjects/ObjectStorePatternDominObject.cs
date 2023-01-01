using System.Collections.Generic;
using System.Linq;

namespace AI.Web.SOAPServiceLibrary.DomainObjects
{
    public class ObjectStorePatternDominObject
    {
        private IDictionary<Identifier, string> rows { get; set; }
        public ObjectStorePatternDominObject()
        {
            rows = new Dictionary<Identifier, string>();
        }
        public ObjectStorePatternDominObject(IDictionary<Identifier, string> rows)
        {
            this.rows = rows;
        }

        public IList<string> GetwhereKeyIs(string key)
        {
            var result = rows.Where(r =>  r.Key.key == key).Select(g => g.Value).ToList();

            return result;
        }

        public IList<string> Getwhere(Identifier ID)
        {
            var result = rows.Where(r => r.Key.key == ID.key && r.Key.ID == ID.ID && r.Key.dateTime == ID.dateTime)
                .Select(g => g.Value).ToList();

            return result;
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

    public class ObjectByteStorePatternDominObject
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
            var result = rows.Where(r => r.Key.key == ID.key && r.Key.ID == ID.ID && r.Key.dateTime == ID.dateTime)
                .Select(g => g.Value).ToList();

            return result;
        }
        public IList<byte[]> GetwhereKeyIs(string key)
        {
            return rows.Where(s => s.Key.key == key)
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
