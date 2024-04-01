using MediatR;
using Newtonsoft.Json;
using StackOverflowTags.Domain.Interfaces;
using System.Net;
using System.Text.Json;

namespace StackOverflowTags.Application.Tag.Queries.Commands.RefreshTags
{
    public class RefreshTagsCommandHandler : IRequestHandler<RefreshTagsCommand>
    {
        private readonly ITagRepository tagRepository;
        private HttpClientHandler handler;

        public RefreshTagsCommandHandler(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;

            handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
        }

        private class ResponseObject()
        {
            public IEnumerable<ResponseTag> items = default!;
        }

        private class ResponseTag()
        {
            public int count;
            public string name = default!;
        }

        public async Task Handle(RefreshTagsCommand request, CancellationToken cancellationToken)
        {
            var existingTags =tagRepository.GetAll();

            if (existingTags.Count() > 0)
            {
                tagRepository.Clear();
            }

            List<ResponseTag> allTags = new();

            for (int i = 1; i <= 10; i++)
            {
                var client = new HttpClient(handler);
                using (var response = client.GetAsync($"https://api.stackexchange.com/2.3/tags?page={i}&pagesize=100&order=desc&sort=popular&site=stackoverflow").Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = await response.Content.ReadAsStringAsync();
                        var parsed = JsonConvert.DeserializeObject<ResponseObject>(responseJson);
                        allTags.AddRange(parsed.items);
                    }
                }
            }

            int totalCount = 0;

            foreach (var item in allTags)
            {
                Console.WriteLine(item.name + ", " + item.count);
                totalCount += item.count;
            }

            List<Domain.Entities.Tag> tags = new List<Domain.Entities.Tag>();

            foreach (var item in allTags)
            {
                float percentage = (float) item.count / totalCount;

                Domain.Entities.Tag newTag = new Domain.Entities.Tag();
                newTag.Name = item.name;
                newTag.Count = item.count;
                newTag.Percentage = percentage;

                tags.Add(newTag);
            }

            tagRepository.Create(tags);
        }
    }
}
