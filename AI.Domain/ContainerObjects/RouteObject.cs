using System;
using System.Collections.Generic;
using System.Text;

namespace AI.Domain.ContainerObjects
{
    public class RouteObject
    {
        public int id { get; private set; }
        public string discription { get; private set; }
        public string iconUrl { get; private set; }
        public string technicalDetails { get; private set; }
        public RouteObject(int id, string discription,
            string iconUrl, string technicalDetails)
        {
            this.id = id;
            this.iconUrl = iconUrl;
            this.technicalDetails = technicalDetails;
            this.discription = discription;
        }
    }
}
