using AutoMapper;

namespace StackOverflowTags.Application.Mappings
{
    public class TagMappingProfile : Profile
    {
        public TagMappingProfile()
        {
            CreateMap<Tag.TagDto, Domain.Entities.Tag>().ReverseMap();
        }
    }
}
