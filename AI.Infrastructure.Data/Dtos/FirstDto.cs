using System;

namespace AI.Infrastructure.Data.Dtos // Dto: Data Transfer Objects, are used to store type specific data in memory for use by other methods
{
    public class DToTypeExample1
    {
        public int id;
        public string name;
        public string description;
        public DateTime dateTime;
    }
    // as this is just a training sample we put classes together like this
    // This is bad practice, rather create new class files for each intended purpose
    public class DToTypeExample2
    {
        private string sdescription = "";
        public DToTypeExample2(string setDescription) // this is known as a constructor for the Dto Object. Constructors run when we use the new keyword to create the object
        {
             sdescription = setDescription;
        }
        public int id { get; set;  }
        public string name { get; set; }
        public string description { get { return this.description.Equals("") ? "Unset" : this.description; }  }
        public DateTime dateTime { get; set; }
    }
}
