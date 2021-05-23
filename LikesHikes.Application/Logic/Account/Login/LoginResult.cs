using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Logic.Account.Login
{
    public class LoginResult
    {
        public string Email { get; set; }

        public string UserName { get; set; }

        public bool IsAdmin { get; set; }

        public int RoutesCount { get; set; }

        public int PassedRoutesCount { get; set; }

        public string Token { get; set; }
    }
}
