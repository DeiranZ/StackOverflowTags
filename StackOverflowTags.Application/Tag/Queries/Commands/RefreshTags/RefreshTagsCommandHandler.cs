using MediatR;
using StackOverflowTags.Domain.Interfaces;

namespace StackOverflowTags.Application.Tag.Queries.Commands.RefreshTags
{
    public class RefreshTagsCommandHandler : IRequestHandler<RefreshTagsCommand>
    {
        private readonly ITagTableInitializer tagTableInitializer;

        public RefreshTagsCommandHandler(ITagTableInitializer tagTableInitializer)
        {
            this.tagTableInitializer = tagTableInitializer;
        }

        public async Task Handle(RefreshTagsCommand request, CancellationToken cancellationToken)
        {
            await tagTableInitializer.Initialize();
        }
    }
}
