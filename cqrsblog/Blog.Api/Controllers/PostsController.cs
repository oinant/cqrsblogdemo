using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blog.Api.ViewModels;
using Blog.Domain.UseCases;
using Blog.Query.DB;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Internal;

namespace Blog.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/articles")]
    public class PostsController : Controller
    {
        private readonly InMemoryQueryDB _queryDataBase;
        private readonly IMediator _mediator;

        public PostsController(InMemoryQueryDB queryDataBase, IMediator mediator)
        {
            _queryDataBase = queryDataBase;
            _mediator = mediator;
        }

        [HttpGet()]
        public IActionResult GetPostSummaries()
        {
            var articles = _queryDataBase.GetAllPostSummaries();
            return Ok(articles);
        }

        [HttpGet("{postId}")]
        public IActionResult GetPost(Guid postId)
        {
            var article = _queryDataBase.GetPost(postId);
            return Ok(article);
        }

        [HttpPut()]
        public async Task<IActionResult> StartPost([FromBody] NewPostViewModel postViewModel )
        {
            var startPostCommand = new StartPostCommand(postViewModel.Author, postViewModel.Title);
            await _mediator.Publish(startPostCommand, CancellationToken.None);
            return Ok();
        }

        [HttpPost("{postId}")]
        public async Task<IActionResult> ChangePostTitle(Guid postId, [FromBody] ChangePostTitleViewModel postViewModel )
        {
            var startPostCommand = new ChangeTitleCommand(postId, postViewModel.Author, postViewModel.NewTitle);
            await _mediator.Publish(startPostCommand, CancellationToken.None);
            return Ok();
        }
    }
}