using AutoMapper;
using MediatR;
using StackOverflowTags.Domain.Interfaces;
using StackOverflowTags.Domain.Models;

namespace StackOverflowTags.Application.Tag.Queries.GetTags
{
    public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, PagedList<TagDto>>
    {
        private readonly ITagRepository tagRepository;
        private readonly IMapper mapper;

        public GetTagsQueryHandler(ITagRepository tagRepository, IMapper mapper)
        {
            this.tagRepository = tagRepository;
            this.mapper = mapper;
        }

        public async Task<PagedList<TagDto>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
        {
            var tags = await tagRepository.Get(request.TagParameters);
            await Console.Out.WriteLineAsync("TotalCount: " + tags.TotalCount);
            var dtos = mapper.Map<PagedList<TagDto>>(tags);
            dtos.TotalCount = tags.TotalCount;
            dtos.TotalPages = tags.TotalPages;
            dtos.CurrentPage = tags.CurrentPage;
            dtos.PageSize = tags.PageSize;

            return dtos;
        }

    }
}
