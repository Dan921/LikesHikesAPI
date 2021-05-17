using LikesHikes.Application.Logic.User.Login;
using LikesHikes.Application.Logic.User.Registration;
using LikesHikes.Application.Models;
using LikesHikes.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LikesHikes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountController : ControllerBase
    {
        private readonly IMediator mediator;

        public AccountController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserAuthModel>> LoginAsync([FromBody] LoginRequest query)
        {
            return await mediator.Send(query);
        }

        [HttpPost("registration")]
        [AllowAnonymous]
        public async Task<ActionResult<UserAuthModel>> RegistrationAsync([FromBody] RegistrationRequest command)
        {
            return await mediator.Send(command);
        }
    }
}
