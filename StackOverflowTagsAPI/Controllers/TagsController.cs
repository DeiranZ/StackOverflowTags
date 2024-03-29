using Microsoft.AspNetCore.Mvc;
using StackOverflowTags.Domain.Interfaces;

namespace StackOverflowTags.API.Controllers
{
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagRepository tagRepository;

        public TagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<ActionResult<IEnumerable<Domain.Entities.Tag>>> GetAll()
        {
            var result = await tagRepository.GetAll();
            if (result == null) { return BadRequest(); }
            return CreatedAtAction(nameof(GetAll), "Works!");
        }
    }
}
