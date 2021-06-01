using Application.Exceptions;
using LikesHikes.Application.Logic.PersonalArea.CreateOrEditReport;
using LikesHikes.Application.Logic.PersonalArea.GetReport;
using LikesHikes.Application.Logic.PersonalArea.GetUserRoutes;
using LikesHikes.Application.Logic.PersonalArea.GetUserRoutesUsingFilter;
using LikesHikes.Application.Logic.PersonalArea.MarkRoutePassed;
using LikesHikes.Application.Logic.PersonalArea.PublishRoute;
using LikesHikes.Application.Logic.PersonalArea.RemoveReport;
using LikesHikes.Application.Logic.PersonalArea.RemoveRoute;
using LikesHikes.Application.Logic.Routs.CreateOrEditRoute;
using LikesHikes.Application.Logic.Routs.GetRouteById;
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
    public class PersonalAreaController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly UserManager<AppUser> userManager;

        public PersonalAreaController(IMediator mediator, UserManager<AppUser> userManager)
        {
            this.mediator = mediator;
            this.userManager = userManager;
        }

        [HttpPut("ChangeRoutePassed")]
        public async Task<IActionResult> ChangeRoutePassed([FromBody] ChangeRoutePassedRequest request)
        {
            request.AppUserId = (await userManager.GetUserAsync(User)).Id;
            return Ok(await mediator.Send(request));
        }

        [HttpPost("CreateOrEditReport")]
        public async Task<IActionResult> CreateOrEditReport([FromBody] CreateOrEditReportRequest request)
        {
            request.AppUserId = (await userManager.GetUserAsync(User)).Id;
            return Ok(await mediator.Send(request));
        }

        [HttpPost("CreateOrEditRoute")]
        public async Task<IActionResult> CreateRoute([FromBody] CreateOrEditRouteRequest request)
        {
            request.AppUserId = (await userManager.GetUserAsync(User)).Id;
            return Ok(await mediator.Send(request));
        }

        [HttpGet("GetReport")]
        public async Task<IActionResult> GetReport([FromQuery] Guid routeId)
        {
            return Ok(await mediator.Send(new GetReportRequest 
            { 
                RouteId = routeId, 
                AppUserId = (await userManager.GetUserAsync(User))?.Id 
            }));
        }

        [HttpGet("GetUserRoutes")]
        public async Task<IActionResult> GetUserRoutes()
        {
            return Ok(await mediator.Send(new GetUserRoutesRequest
            {
                AppUserId = (await userManager.GetUserAsync(User))?.Id
            }));
        }

        [HttpPost("GetUserRoutesUsingFilter")]
        public async Task<IActionResult> GetUserRoutesUsingFilter([FromBody] UserRouteFilterModel filter)
        {
            return Ok(await mediator.Send(new GetUserRoutesUsingFilterRequest
            {
                AppUserId = (await userManager.GetUserAsync(User))?.Id,
                Filter = filter
            }));
        }

        [HttpPut("PublishRoute")]
        public async Task<IActionResult> PublishRoute([FromBody] PublishRouteRequest request)
        {
            request.AppUserId = (await userManager.GetUserAsync(User))?.Id;
            return Ok(await mediator.Send(request));
        }

        [HttpDelete("RemoveRoute")]
        public async Task<IActionResult> RemoveRoute([FromQuery] Guid routeId)
        {
            return Ok(await mediator.Send(new RemoveRouteRequest 
            {
                AppUserId = (await userManager.GetUserAsync(User))?.Id,
                RouteId = routeId
            }));
        }

        [HttpDelete("RemoveReport")]
        public async Task<IActionResult> RemoveReport([FromQuery] Guid routeId)
        {
            return Ok(await mediator.Send(new RemoveReportRequest
            {
                AppUserId = (await userManager.GetUserAsync(User))?.Id,
                RouteId = routeId
            }));
        }
    }
}
