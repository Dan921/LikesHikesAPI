using Application.Exceptions;
using LikesHikes.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LikesHikes.Api.Middleware
{
    public class UserVerificationMiddleware
    {
        private readonly RequestDelegate next;

        public UserVerificationMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserManager<AppUser> userManager)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if(token != null)
            {
                var user = await userManager.GetUserAsync(context.User);

                if (user == null)
                {
                    throw new RestException("Пользователь не найден");
                }
            }

            await next(context);
        }
    }
}
