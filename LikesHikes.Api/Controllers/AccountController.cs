using Application.Exceptions;
using LikesHikes.Application.Logic.Account.GetUserData;
using LikesHikes.Application.Logic.Account.Login;
using LikesHikes.Application.Logic.Account.Registration;
using LikesHikes.Application.Models;
using LikesHikes.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LikesHikes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly UserManager<AppUser> userManager;

        public AccountController(IMediator mediator, UserManager<AppUser> userManager)
        {
            this.mediator = mediator;
            this.userManager = userManager;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest query)
        {
            return Ok(await mediator.Send(query));
        }

        [HttpPost("registration")]
        [AllowAnonymous]
        public async Task<IActionResult> RegistrationAsync([FromBody] RegistrationRequest command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpGet("GetUserData")]
        public async Task<IActionResult> GetUserData()
        {
            return Ok(await mediator.Send(new GetUserDataRequest
            {
                AppUserId = (await userManager.GetUserAsync(User))?.Id
            }));
        }
    }
}
