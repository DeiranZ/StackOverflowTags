using Newtonsoft.Json;
using StackOverflowTags.Domain.Interfaces;
using System.Net;

namespace StackOverflowTags.Infrastructure.TagTableInitializer
{
    public class TagTableInitializer : ITagTableInitializer
    {
        private readonly ITagRepository tagRepository;
        private HttpClientHandler handler;

        public TagTableInitializer(ITagRepository tagRepository) 
        {
            this.tagRepository = tagRepository;

            handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
        }

        public async Task Initialize()
        {
            var existingTags = tagRepository.GetAll();

            if (existingTags.Count() > 0)
            {
                return;
            }

            await LoadTags();
        }

        public async Task Reinitialize()
        {
            var existingTags = tagRepository.GetAll();

            if (existingTags.Count() > 0)
            {
                tagRepository.Clear();
            }

            await LoadTags();
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

        public async Task LoadTags()
        {
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

            List<Domain.Entities.Tag> tags = new();

            foreach (var item in allTags)
            {
                float percentage = (float)item.count / totalCount;

                Domain.Entities.Tag newTag = new();
                newTag.Name = item.name;
                newTag.Count = item.count;
                newTag.Percentage = percentage * 100f;

                tags.Add(newTag);
            }

            tagRepository.Create(tags);
        }
    }
}

