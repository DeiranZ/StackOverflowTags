using Microsoft.EntityFrameworkCore;
using StackOverflowTags.Infrastructure.Persistence;
using StackOverflowTags.Infrastructure.Repositories;
using Testcontainers.MsSql;
using FluentAssertions;

namespace StackOverflowTags.IntegrationTests.Repositories
{
    public class TagRepositoryTests
    {
        [Fact]
        public async Task CreateTag_HasCorrectData()
        {
            // arrange

            var container = new MsSqlBuilder()
                .WithImage("mcr.microsoft.com/mssql/server:2022-CU12-ubuntu-22.04")
                .WithPassword("Rootpas$")
                .Build();

            await container.StartAsync();

            var context = new StackOverflowTagsDbContext(new DbContextOptionsBuilder<StackOverflowTagsDbContext>()
                .UseSqlServer(container.GetConnectionString()).Options);

            context.Database.EnsureCreated();

            var repo = new TagRepository(context);

            var newTag = new Domain.Entities.Tag { Name = "java", Count = 5000, Percentage = 4.25f };

            // act

            await repo.Create(newTag);

            // assert

            var dbTags = await context.Tags.ToListAsync();

            dbTags.Should().HaveCount(1);
            dbTags[0].Name.Should().Be(newTag.Name);
            dbTags[0].Count.Should().Be(newTag.Count);
            dbTags[0].Percentage.Should().Be(newTag.Percentage);
        }

        [Fact()]
        public async Task CreateBunch_HasCorrectData()
        {
            // arrange

            var container = new MsSqlBuilder()
                .WithImage("mcr.microsoft.com/mssql/server:2022-CU12-ubuntu-22.04")
                .WithPassword("Rootpas$")
                .Build();

            await container.StartAsync();

            var context = new StackOverflowTagsDbContext(new DbContextOptionsBuilder<StackOverflowTagsDbContext>()
                .UseSqlServer(container.GetConnectionString()).Options);

            context.Database.EnsureCreated();

            var repo = new TagRepository(context);

            var tags = new List<Domain.Entities.Tag>() {
                new Domain.Entities.Tag() { Name = "java", Count = 5000, Percentage = 4.25f },
                new Domain.Entities.Tag() { Name = "csharp", Count = 3000, Percentage = 1.7f },
                new Domain.Entities.Tag() { Name = "cpp", Count = 1000, Percentage = 0.5f },
            };

            // act

            await repo.Create(tags);

            // assert

            var dbTags = await context.Tags.ToListAsync();

            dbTags.Should().HaveCount(3);
            dbTags[0].Name.Should().Be(tags[0].Name);
            dbTags[0].Count.Should().Be(tags[0].Count);
            dbTags[0].Percentage.Should().Be(tags[0].Percentage);

            dbTags[1].Name.Should().Be(tags[1].Name);
            dbTags[1].Count.Should().Be(tags[1].Count);
            dbTags[1].Percentage.Should().Be(tags[1].Percentage);

            dbTags[2].Name.Should().Be(tags[2].Name);
            dbTags[2].Count.Should().Be(tags[2].Count);
            dbTags[2].Percentage.Should().Be(tags[2].Percentage);
        }
    }
}
