using e_Estoque_API.Application.Categories.Commands;
using e_Estoque_API.Application.Categories.Commands.Handlers;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Fakes;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Infrastructure.MessageBus;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace e_Estoque_API.UnitTest.Categories.Commands.Handlers;

public class CreateCategoryCommandHandlerTest : BaseTest, IClassFixture<BaseTest>
{
    private readonly ServiceProvider _serviceProvide;

    public CreateCategoryCommandHandlerTest(BaseTest baseTest)
    {
        _serviceProvide = baseTest.ServiceProvider;
    }

    [Fact(DisplayName = "Test create category success")]
    [Trait("CreateCategoryCommandHandlerTest", "CreateCategory CommandHandler Tests")]
    public async Task CreateCategoryCommandHandlerSuccess()
    {
        // Arrange
        var categoryRepository = _serviceProvide.GetService<ICategoryRepository>();
        var messageBusMock = new Mock<IMessageBusClient>();
        var faker = new GenerateCategoryFake().CreateValid(1).First();

        // Act
        var commandHandler = new CreateCategoryCommandHandler(categoryRepository, messageBusMock.Object);

        var command = new CreateCategoryCommand
        {
            Name = faker.Name,
            Description = faker.Description,
            ShortDescription = faker.ShortDescription
        };

        var result = await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        categoryRepository.Should().NotBeNull();
        result.Should().NotBeEmpty();
    }

    [Fact(DisplayName = "Test create category failure")]
    [Trait("CreateCategoryCommandHandlerTest", "CreateCategory CommandHandler Tests")]
    public async Task CreateCategoryCommandHandlerFailure()
    {
        // Arrange
        var categoryRepository = _serviceProvide.GetService<ICategoryRepository>();
        var messageBusMock = new Mock<IMessageBusClient>();
        var faker = new GenerateCategoryFake().CreateInValid(1).First();

        // Act
        var commandHandler = new CreateCategoryCommandHandler(categoryRepository, messageBusMock.Object);

        var command = new CreateCategoryCommand
        {
            Name = faker.Name,
            Description = faker.Description,
            ShortDescription = faker.ShortDescription
        };

        Func<Task> act = async () => await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        categoryRepository.Should().NotBeNull();
        await act.Should().ThrowAsync<ValidationException>();
    }
}
