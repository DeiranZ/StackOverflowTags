using MediatR;
using StackOverflowTags.Domain.Models;

namespace StackOverflowTags.Application.Tag.Queries.GetTags
{
    public class GetTagsQuery : IRequest<PagedList<TagDto>>
    {
        public GetTagsQuery(TagParameters parameters)
        {
            TagParameters = parameters;
        }

        public TagParameters TagParameters { get; set; } = default!;
    }
}
