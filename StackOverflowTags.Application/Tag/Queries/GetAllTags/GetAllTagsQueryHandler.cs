using AutoMapper;
using MediatR;
using StackOverflowTags.Domain.Interfaces;

namespace StackOverflowTags.Application.Tag.Queries.GetAllTags
{
    public class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, IEnumerable<TagDto>>
    {
        private readonly ITagRepository tagRepository;
        private readonly IMapper mapper;

        public GetAllTagsQueryHandler(ITagRepository tagRepository, IMapper mapper) 
        {
            this.tagRepository = tagRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<TagDto>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
        {
            var tags = await tagRepository.GetAll();
            var dtos = mapper.Map<IEnumerable<TagDto>>(tags);

            return dtos;
        }
    }
}
