using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Domain
{
    public interface IEntity
    {
        public Guid Id { get; set; }
    }
}
