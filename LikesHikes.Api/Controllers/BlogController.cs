using Application.Exceptions;
using LikesHikes.Api.Attributes;
using LikesHikes.Application.Logic.Blog.CreateBlogPost;
using LikesHikes.Application.Logic.Blog.CreateBlogPostComment;
using LikesHikes.Application.Logic.Blog.GetBlogPostById;
using LikesHikes.Application.Logic.Blog.GetBlogPosts;
using LikesHikes.Application.Logic.Blog.RemoveBlogPost;
using LikesHikes.Application.Logic.Blog.RemoveBlogPostComment;
using LikesHikes.Application.Logic.Blog.UpdateBlogPost;
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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LikesHikes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BlogController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly UserManager<AppUser> userManager;

        public BlogController(IMediator mediator, UserManager<AppUser> userManager)
        {
            this.mediator = mediator;
            this.userManager = userManager;
        }

        [AuthorizeRoles(UserRole.Admin)]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequest request)
        {
            return Ok(await mediator.Send(request));
        }

        [AuthorizeRoles(UserRole.Admin)]
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteBlogPost([FromQuery] Guid postId)
        {
            return Ok(await mediator.Send(new RemoveBlogPostRequest { Id = postId }));
        }

        [AllowAnonymous]
        [HttpGet("Posts")]
        public async Task<IActionResult> GetBlogPost([FromQuery] int page = 1)
        {
            return Ok(await mediator.Send(new GetBlogPostsRequest { Page = page }));
        }

        [AllowAnonymous]
        [HttpGet("Post")]
        public async Task<IActionResult> GetBlogPostById([FromQuery] Guid id)
        {
            return Ok(await mediator.Send(new GetBlogPostByIdRequest { Id = id }));
        }

        [AuthorizeRoles(UserRole.Admin)]
        [HttpPut("Edit")]
        public async Task<IActionResult> UpdateBlogPost([FromBody] UpdateBlogPostRequest request)
        {
            return Ok(await mediator.Send(request));
        }

        [HttpPost("CreateComment")]
        public async Task<IActionResult> CreateBlogPostComment([FromBody] CreateBlogPostCommentRequest request)
        {
            request.UserId = (await userManager.GetUserAsync(User))?.Id;
            return Ok(await mediator.Send(request));
        }

        [HttpDelete("RemoveComment")]
        public async Task<IActionResult> RemoveBlogPostComment([FromQuery] Guid id)
        {
            return Ok(await mediator.Send(new RemoveBlogPostCommentRequest { Id = id }));
        }
    }
}
