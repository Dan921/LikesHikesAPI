using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Domain.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
    }
}
