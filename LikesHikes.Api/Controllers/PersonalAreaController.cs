using Application.Exceptions;
using LikesHikes.Application.Logic.PersonalArea.CreateReport;
using LikesHikes.Application.Logic.PersonalArea.GetReport;
using LikesHikes.Application.Logic.PersonalArea.MarkRoutePassed;
using LikesHikes.Application.Logic.PersonalArea.PublishRoute;
using LikesHikes.Application.Logic.PersonalArea.RemoveReport;
using LikesHikes.Application.Logic.PersonalArea.RemoveRoute;
using LikesHikes.Application.Logic.PersonalArea.UpdateReport;
using LikesHikes.Application.Logic.Routs.CreateRout;
using LikesHikes.Application.Logic.Routs.GetRouteById;
using LikesHikes.Application.Logic.Routs.UpdateRout;
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

        [HttpPost("CreateReport")]
        public async Task<IActionResult> CreateReport([FromBody] CreateReportRequest request)
        {
            request.AppUserId = (await userManager.GetUserAsync(User)).Id;
            return Ok(await mediator.Send(request));
        }

        [HttpPost("CreateRoute")]
        public async Task<IActionResult> CreateRoute([FromBody] CreateRouteRequest request)
        {
            request.CreatedById = (await userManager.GetUserAsync(User)).Id;
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

        [HttpPut("UpdateReport")]
        public async Task<IActionResult> UpdateReport([FromBody] UpdateReportRequest request)
        {
            request.AppUserId = (await userManager.GetUserAsync(User))?.Id;
            return Ok(await mediator.Send(request));
        }

        [HttpPut("UpdateRoute")]
        public async Task<IActionResult> UpdateRoute([FromBody] UpdateRouteRequest request)
        {
            request.AppUserId = (await userManager.GetUserAsync(User))?.Id;
            return Ok(await mediator.Send(request));
        }
    }
}
