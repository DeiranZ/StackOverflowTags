using AutoMapper;
using MediatR;
using StackOverflowTags.Application.Tag.Queries.GetAllTags;
using StackOverflowTags.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowTags.Application.Tag.Queries.GetTags
{
    public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, IEnumerable<TagDto>>
    {
        private readonly ITagRepository tagRepository;
        private readonly IMapper mapper;

        public GetTagsQueryHandler(ITagRepository tagRepository, IMapper mapper)
        {
            this.tagRepository = tagRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<TagDto>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
        {
            var tags = await tagRepository.Get(request.TagParameters);
            var dtos = mapper.Map<IEnumerable<TagDto>>(tags);

            return dtos;
        }

    }
}
