using Microsoft.AspNetCore.Mvc;
using StackOverflowTags.Application.Tag;
using MediatR;
using StackOverflowTags.Application.Tag.Queries.GetAllTags;
using StackOverflowTags.Application.Tag.Queries.Commands.RefreshTags;

namespace StackOverflowTags.API.Controllers
{
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly IMediator mediator;

        public TagsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<TagDto>>> GetAll()
        {
            var result = await mediator.Send(new GetAllTagsQuery());
            if (result == null) { return BadRequest(); }
            return CreatedAtAction(nameof(GetAll), result);
        }

        [HttpPost]
        [Route("RefreshData")]
        public async void RefreshData()
        {
            await mediator.Send(new RefreshTagsCommand());
        }
    }
}
