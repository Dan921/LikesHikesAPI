using LikesHikes.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Logic.Account.GetUserData
{
    public class GetUserDataResult
    {
        public string Email { get; set; }

        public string UserName { get; set; }

        public bool IsAdmin { get; set; }

        public int RoutesCount { get; set; }

        public int PassedRoutesCount { get; set; }
    }
}
