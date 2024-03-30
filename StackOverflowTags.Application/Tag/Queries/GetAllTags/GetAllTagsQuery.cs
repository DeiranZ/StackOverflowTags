using MediatR;

namespace StackOverflowTags.Application.Tag.Queries.GetAllTags
{
    public class GetAllTagsQuery : IRequest<IEnumerable<TagDto>>
    {
    }
}
