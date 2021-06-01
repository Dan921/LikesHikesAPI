using Application.Exceptions;
using LikesHikes.Api.Attributes;
using LikesHikes.Application.Logic.Routs.AddRouteToUser;
using LikesHikes.Application.Logic.Routs.CreateRoutReview;
using LikesHikes.Application.Logic.Routs.GetRouteById;
using LikesHikes.Application.Logic.Routs.GetRoutes;
using LikesHikes.Application.Logic.Routs.GetRoutesUsingFilter;
using LikesHikes.Application.Logic.Routs.RemoveRout;
using LikesHikes.Application.Logic.Routs.RemoveRouteReview;
using LikesHikes.Application.Models;
using LikesHikes.Domain.Entities;
using LikesHikes.Domain.Models;
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
    public class RoutesController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly UserManager<AppUser> userManager;

        public RoutesController(IMediator mediator, UserManager<AppUser> userManager)
        {
            this.mediator = mediator;
            this.userManager = userManager;
        }

        [HttpPost("CreateReview")]
        public async Task<IActionResult> CreateReview([FromBody] CreateRouteReviewRequest request)
        {
            request.AppUserId = (await userManager.GetUserAsync(User))?.Id;
            return Ok(await mediator.Send(request));
        }

        [AllowAnonymous]
        [HttpGet("GetAllRoutes")]
        public async Task<IActionResult> GetAllRoutes()
        {
            return Ok(await mediator.Send(new GetAllRoutesRequest 
            {
                AppUserId = (await userManager.GetUserAsync(User))?.Id
            }));
        }

        [AllowAnonymous]
        [HttpPost("GetRoutesUsingFilter")]
        public async Task<IActionResult> GetRoutesUsingFilter([FromBody] RouteFilterModel filter)
        {
            return Ok(await mediator.Send(new GetRoutesUsingFilterRequest
            {
                AppUserId = (await userManager.GetUserAsync(User))?.Id,
                RouteFilter = filter
            }));
        }

        [AllowAnonymous]
        [HttpGet("GetRouteById")]
        public async Task<IActionResult> GetRouteById([FromQuery] Guid id)
        {
            return Ok(await mediator.Send(new GetRouteByIdRequest 
            { 
                AppUserId = (await userManager.GetUserAsync(User))?.Id, 
                RouteId = id 
            }));
        }

        [AuthorizeRoles(UserRole.Admin)]
        [HttpDelete("RemoveRoute")]
        public async Task<IActionResult> RemoveRoute([FromQuery] Guid routeId)
        {
            return Ok(await mediator.Send(new RemoveRoutRequest { Id = routeId }));
        }

        [HttpDelete("RemoveRouteReview")]
        public async Task<IActionResult> RemoveRouteReview([FromQuery] Guid routeReviewId)
        {
            var user = await userManager.GetUserAsync(User);
            return Ok(await mediator.Send(new RemoveRouteReviewRequest 
            { 
                Id = routeReviewId,
                AppUserId = user?.Id,
                IsAdmin = await userManager.IsInRoleAsync(user, nameof(UserRole.Admin))
            }));
        }

        [HttpPost("AddRouteToUser")]
        public async Task<IActionResult> AddRouteToUser([FromBody] AddRouteToUserRequest request)
        {
            request.AppUserId = (await userManager.GetUserAsync(User))?.Id;
            return Ok(await mediator.Send(request));
        }
    }
}
