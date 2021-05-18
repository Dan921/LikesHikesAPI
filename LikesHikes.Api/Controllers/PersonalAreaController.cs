using LikesHikes.Application.Logic.PersonalArea.CreateReport;
using LikesHikes.Application.Logic.PersonalArea.GetReport;
using LikesHikes.Application.Logic.PersonalArea.GetUserData;
using LikesHikes.Application.Logic.PersonalArea.MarkRoutePassed;
using LikesHikes.Application.Logic.PersonalArea.PublishRoute;
using LikesHikes.Application.Logic.PersonalArea.RemoveReport;
using LikesHikes.Application.Logic.PersonalArea.RemoveRoute;
using LikesHikes.Application.Logic.PersonalArea.UpdateReport;
using LikesHikes.Application.Logic.Routs.CreateRout;
using LikesHikes.Application.Logic.Routs.UpdateRout;
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
        public async Task<Unit> ChangeRoutePassed([FromBody] ChangeRoutePassedRequest request)
        {
            try
            {
                var user = await userManager.GetUserAsync(User);
                request.AppUserId = user.Id;
                return await mediator.Send(request);
            }
            catch
            {
                throw new ApplicationException("User is not found");
            }
        }

        [HttpPost("CreateReport")]
        public async Task<Unit> CreateReport([FromBody] CreateReportRequest request)
        {
            try
            {
                var user = await userManager.GetUserAsync(User);
                request.AppUserId = user.Id;
                return await mediator.Send(request);
            }
            catch
            {
                throw new ApplicationException("User is not found");
            }
        }

        [HttpPost("CreateRoute")]
        public async Task<Unit> CreateRoute([FromBody] CreateRouteRequest request)
        {
            try
            {
                var user = await userManager.GetUserAsync(User);
                request.CreatedById = user.Id;
                return await mediator.Send(request);
            }
            catch
            {
                throw new ApplicationException("User is not found");
            }
        }

        [HttpGet("GetReport/route={id}")]
        public async Task<ReportModel> GetReport([FromRoute] Guid id)
        {
            try
            {
                var user = await userManager.GetUserAsync(User);
                return await mediator.Send(new GetReportRequest { RouteId = id, AppUserId = user.Id });
            }
            catch
            {
                throw new ApplicationException("User is not found");
            }
        }

        [HttpGet("GetUserData")]
        public async Task<GetUserDataResult> GetUserData([FromQuery] bool OnlyPassedRoutes = false)
        {
            try
            {
                var user = await userManager.GetUserAsync(User);
                return await mediator.Send(new GetUserDataRequest { AppUserId = user.Id, OnlyPassedRoutes = OnlyPassedRoutes });
            }
            catch
            {
                throw new ApplicationException("User is not found");
            }
        }

        [HttpPut("PublishRoute")]
        public async Task<Unit> PublishRoute([FromBody] PublishRouteRequest request)
        {
            try
            {
                var user = await userManager.GetUserAsync(User);
                request.AppUserId = user.Id;
                return await mediator.Send(request);
            }
            catch
            {
                throw new ApplicationException("User is not found");
            }
        }

        [HttpDelete("RemoveRoute")]
        public async Task<Unit> RemoveRoute([FromBody] RemoveReportRequest request)
        {
            try
            {
                var user = await userManager.GetUserAsync(User);
                request.AppUserId = user.Id;
                return await mediator.Send(request);
            }
            catch
            {
                throw new ApplicationException("User is not found");
            }
        }

        [HttpDelete("RemoveReport")]
        public async Task<Unit> RemoveReport([FromBody] RemoveRouteRequest request)
        {
            try
            {
                var user = await userManager.GetUserAsync(User);
                request.AppUserId = user.Id;
                return await mediator.Send(request);
            }
            catch
            {
                throw new ApplicationException("User is not found");
            }
        }

        [HttpPut("UpdateReport")]
        public async Task<ReportModel> UpdateReport([FromBody] UpdateReportRequest request)
        {
            var user = await userManager.GetUserAsync(User);
            request.AppUserId = user.Id;
            return await mediator.Send(request);
        }

        [HttpPut("UpdateRoute")]
        public async Task<RouteDetailModel> UpdateRoute([FromBody] UpdateRouteRequest request)
        {
            try
            {
                var user = await userManager.GetUserAsync(User);
                request.AppUserId = user.Id;
                return await mediator.Send(request);
            }
            catch
            {
                throw new ApplicationException("User is not found");
            }
        }
    }
}
