using LikesHikes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LikesHikes.Domain
{
    public interface IJwtGenerator
    {
        Task<string> CreateToken(AppUser user);
    }
}
