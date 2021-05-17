using LikesHikes.Application.Logic.Routs.CreateRoutReview;
using LikesHikes.Application.Logic.Routs.GetRouteById;
using LikesHikes.Application.Logic.Routs.GetRoutes;
using LikesHikes.Application.Logic.Routs.RemoveRout;
using LikesHikes.Application.Logic.Routs.RemoveRouteReview;
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
        public async Task<Unit> CreateReview([FromBody] CreateRouteReviewRequest request)
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

        [AllowAnonymous]
        [HttpGet("GetAllRoutes")]
        public async Task<IEnumerable<RouteShortModel>> GetAllRoutes([FromBody] GetAllRoutesRequest request)
        {
            return await mediator.Send(request);
        }

        [AllowAnonymous]
        [HttpGet("GetRouteById")]
        public async Task<RouteDetailModel> GetRouteById([FromBody] GetRouteByIdRequest request)
        {
            return await mediator.Send(request);
        }

        [HttpDelete("RemoveRoute")]
        public async Task<Unit> RemoveRoute([FromBody] RemoveRoutRequest request)
        {
            return await mediator.Send(request);
        }

        [HttpDelete("RemoveRouteReview")]
        public async Task<Unit> RemoveRouteReview([FromBody] RemoveRouteReviewRequest request)
        {
            return await mediator.Send(request);
        }
    }
}
