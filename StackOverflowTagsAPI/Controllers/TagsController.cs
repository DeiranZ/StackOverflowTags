using Microsoft.AspNetCore.Mvc;
using StackOverflowTags.Application.Tag;
using MediatR;
using StackOverflowTags.Application.Tag.Queries.GetAllTags;

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
        [Route("getall")]
        public async Task<ActionResult<IEnumerable<TagDto>>> GetAll()
        {
            var result = await mediator.Send(new GetAllTagsQuery());
            if (result == null) { return BadRequest(); }
            return CreatedAtAction(nameof(GetAll), "Mediator Works!");
        }

        [HttpPost]
        [Route("refreshdata")]
        public void RefreshData()
        {

        }
    }
}
