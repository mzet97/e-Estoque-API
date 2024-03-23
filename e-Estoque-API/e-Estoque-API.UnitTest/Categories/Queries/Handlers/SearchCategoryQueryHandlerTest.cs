using e_Estoque_API.Application.Categories.Queries;
using e_Estoque_API.Application.Categories.Queries.Handlers;
using e_Estoque_API.Core.Fakes;
using e_Estoque_API.Core.Repositories;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace e_Estoque_API.UnitTest.Categories.Queries.Handlers
{
    public class SearchCategoryQueryHandlerTest : BaseTest, IClassFixture<BaseTest>
    {
        private readonly ServiceProvider _serviceProvide;

        public SearchCategoryQueryHandlerTest(BaseTest baseTest)
        {
            _serviceProvide = baseTest.ServiceProvider;
        }

        [Fact(DisplayName = "Test search category success")]
        [Trait("SearchCategoryQueryHandlerTest", "SearchCategory QueryHandler Tests")]
        public async Task SearchCategoryQueryHandlerSuccess()
        {
            // Arrange
            var categoryRepository = _serviceProvide.GetService<ICategoryRepository>();
            var faker = new GenerateCategoryFake().CreateValid(1).First();

            await categoryRepository.Add(faker);

            // Act
            var queryHandler = new SearchCategoryQueryHandler(categoryRepository);

            var query = new SearchCategoryQuery
            {
                 Name= faker.Name,
                Description= faker.Description,
                ShortDescription= faker.ShortDescription,
                Id= faker.Id,
                Order= "Id"
            };

            var result = await queryHandler.Handle(query, CancellationToken.None);

            // Assert
            categoryRepository.Should().NotBeNull();
            result.Should().NotBeNull();
        }

        [Fact(DisplayName = "Test search category failure")]
        [Trait("SearchCategoryQueryHandlerTest", "SearchCategory QueryHandler Tests")]
        public async Task SearchCategoryQueryHandlerFailure()
        {
            // Arrange
            var categoryRepository = _serviceProvide.GetService<ICategoryRepository>();

            // Act
            var queryHandler = new SearchCategoryQueryHandler(categoryRepository);

            var query = new SearchCategoryQuery
            {
                Id = Guid.NewGuid(),
                Order = "Id"
            };

            var result = await queryHandler.Handle(query, CancellationToken.None);

            // Assert
            categoryRepository.Should().NotBeNull();
            result.Data.Should().BeEmpty();
        }
    }
}
