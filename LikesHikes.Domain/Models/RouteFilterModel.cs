using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Domain.Models
{
    public class RouteFilterModel
    {
        public string NameRoute { get; set; }

        public string Complexity { get; set; }

        public string Region { get; set; }

        public string KeyPoints { get; set; }

        public float? Rating { get; set; }
    }
}
