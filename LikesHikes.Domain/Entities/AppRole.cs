using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Domain.Entities
{
    public class AppRole : IdentityRole<Guid>
    {
        public AppRole() : base() { }

        public AppRole(string name)
            : base(name)
        { }
    }
}
