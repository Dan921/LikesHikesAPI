using LikesHikes.Api.Attributes;
using LikesHikes.Application.Logic.Images.Posts.AddImageToBlogPost;
using LikesHikes.Application.Logic.Images.Posts.GetImage;
using LikesHikes.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class ImagesController : ControllerBase
    {
        private readonly IMediator mediator;

        public ImagesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet("GetImage")]
        public async Task<IActionResult> GetImage([FromQuery] Guid id)
        {
            var image = await mediator.Send(new GetImageRequest { Id = id });
            return File(image, "image/png");
        }

        [AuthorizeRoles(UserRole.Admin)]
        [HttpPost("AddImageToBlogPost/{id}")]
        public async Task<IActionResult> AddImageToBlogPost([FromForm]IFormFile image, [FromRoute] Guid id)
        {
            return Ok(await mediator.Send(new AddImageToBlogPostRequest { Image = image, BlogPostId = id }));
        }
    }
}
