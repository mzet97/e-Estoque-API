using e_Estoque_API.Application.Categories.Queries;
using e_Estoque_API.Application.Categories.Queries.Handlers;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Fakes;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Infrastructure.MessageBus;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace e_Estoque_API.UnitTest.Categories.Queries.Handlers;

public class GetByIdCategoryQueryHandlerTest : BaseTest, IClassFixture<BaseTest>
{
    private readonly ServiceProvider _serviceProvide;

    public GetByIdCategoryQueryHandlerTest(BaseTest baseTest)
    {
        _serviceProvide = baseTest.ServiceProvider;
    }

    [Fact(DisplayName = "Test get by id category success")]
    [Trait("GetByIdCategoryQueryHandlerTest", "GetByIdCategory QueryHandler Tests")]
    public async Task GetByIdCategoryQueryHandlerSuccess()
    {
        // Arrange
        var categoryRepository = _serviceProvide.GetService<ICategoryRepository>();
        var messageBusMock = new Mock<IMessageBusClient>();
        var faker = new GenerateCategoryFake().CreateValid(1).First();

        await categoryRepository.AddAsync(faker);

        // Act
        var queryHandler = new GetByIdCategoryQueryHandler(categoryRepository, messageBusMock.Object);

        var query = new GetByIdCategoryQuery(faker.Id);

        var result = await queryHandler.Handle(query, CancellationToken.None);

        // Assert
        categoryRepository.Should().NotBeNull();
        result.Should().NotBeNull();
    }

    [Fact(DisplayName = "Test get by id category failure")]
    [Trait("GetByIdCategoryQueryHandlerTest", "GetByIdCategory QueryHandler Tests")]
    public async Task GetByIdCategoryQueryHandlerFailure()
    {
        // Arrange
        var categoryRepository = _serviceProvide.GetService<ICategoryRepository>();
        var messageBusMock = new Mock<IMessageBusClient>();
        var faker = new GenerateCategoryFake().CreateInValid(1).First();

        // Act
        var queryHandler = new GetByIdCategoryQueryHandler(categoryRepository, messageBusMock.Object);

        var query = new GetByIdCategoryQuery(faker.Id);

        Func<Task> act = async () => await queryHandler.Handle(query, CancellationToken.None);

        // Assert
        categoryRepository.Should().NotBeNull();
        await act.Should().ThrowAsync<NotFoundException>();
    }
}
