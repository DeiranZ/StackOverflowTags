using Microsoft.AspNetCore.Mvc;
using StackOverflowTags.Application.Tag;
using MediatR;
using StackOverflowTags.Application.Tag.Commands.RefreshTags;
using StackOverflowTags.Domain.Models;
using StackOverflowTags.Application.Tag.Queries.GetTags;

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

        /// <summary>
        /// Gets the list of Tags
        /// </summary>
        /// <returns>The list of Tags</returns>
        /// <response code="201">Returns the list of tags</response>
        /// <response code="400">If the list is null</response>
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<TagDto>>> Get([FromQuery] TagParameters tagParameters)
        {
            if (tagParameters.PageNumber < 1)
            {
                tagParameters.PageNumber = 1;
            }

            var result = await mediator.Send(new GetTagsQuery(tagParameters));
            if (result == null) { return BadRequest(); }
            return CreatedAtAction(nameof(Get), result);
        }

        /// <summary>
        /// Refreshes the list of Tags by loading them again from StackOverflow
        /// </summary>
        [HttpPost]
        [Route("RefreshData")]
        public async void RefreshData()
        {
            await mediator.Send(new RefreshTagsCommand());
        }
    }
}
