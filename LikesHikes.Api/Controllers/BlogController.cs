using LikesHikes.Application.Logic.Blog.CreateBlogPost;
using LikesHikes.Application.Logic.Blog.CreateBlogPostComment;
using LikesHikes.Application.Logic.Blog.GetBlogPostById;
using LikesHikes.Application.Logic.Blog.GetBlogPosts;
using LikesHikes.Application.Logic.Blog.RemoveBlogPost;
using LikesHikes.Application.Logic.Blog.RemoveBlogPostComment;
using LikesHikes.Application.Logic.Blog.UpdateBlogPost;
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

        [HttpPost("Create")]
        public async Task<ActionResult<Unit>> CreateBlogPost([FromBody] CreateBlogPostRequest request)
        {
            return await mediator.Send(request);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<Unit>> DeleteBlogPost([FromBody] RemoveBlogPostRequest request)
        {
            return await mediator.Send(request);
        }

        [HttpGet("Posts/page={page}")]
        public async Task<ActionResult<GetBlogPostsResult>> GetBlogPost([FromRoute] int page)
        {
            return await mediator.Send(new GetBlogPostsRequest { Page = page });
        }

        [HttpGet("Posts/{id}")]
        public async Task<ActionResult<BlogPostDetailModel>> GetBlogPostById([FromRoute] Guid id)
        {
            return await mediator.Send(new GetBlogPostByIdRequest{ Id = id });
        }

        [HttpPut("Edit")]
        public async Task<ActionResult<BlogPostDetailModel>> UpdateBlogPost([FromBody] UpdateBlogPostRequest request)
        {
            return await mediator.Send(request);
        }

        [HttpPost("CreateComment")]
        public async Task<ActionResult<Unit>> CreateBlogPostComment([FromBody] CreateBlogPostCommentRequest request)
        {
            try
            {
                var user = await userManager.GetUserAsync(User);
                request.UserId = user.Id;
                return await mediator.Send(request);
            }
            catch
            {
                throw new ApplicationException("User is not found");
            }
        }

        [HttpDelete("Comments/{id}/Delete")]
        public async Task<ActionResult<Unit>> RemoveBlogPostComment([FromRoute] Guid id)
        {
            return await mediator.Send(new RemoveBlogPostCommentRequest { Id = id });
        }
    }
}
