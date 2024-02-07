using e_Estoque_API.Application.Categories.Commands;
using e_Estoque_API.Application.Categories.Commands.Handlers;
using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Infrastructure.MessageBus;
using FluentAssertions;
using Moq;

namespace e_Estoque_API.UnitTest.Categories.Commands.Handlers;

public class CreateCategoryCommandHandlerTest
{

    [Fact(DisplayName = "Test create category success")]
    [Trait("CreateCategoryCommandHandlerTest", "CreateCategory CommandHandler Tests")]
    public async Task CreateCategoryCommandHandler_Success()
    {
        // Arrange
        var categoryRepositoryMock = new Mock<ICategoryRepository>();
        var messageBusMock = new Mock<IMessageBusClient>();

        // Act
        var commandHandler = new CreateCategoryCommandHandler(categoryRepositoryMock.Object, messageBusMock.Object);

        var command = new CreateCategoryCommand
        {
            Name = "Category Test",
            Description = "Category Test Description",
            ShortDescription = "Category Test Short Description"
        };

        var result = await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();

        categoryRepositoryMock.Verify(pr => pr.Add(It.IsAny<Category>()), Times.Once);
    }

    [Fact(DisplayName = "Test create category failure")]
    [Trait("CreateCategoryCommandHandlerTest", "CreateCategory CommandHandler Tests")]
    public async Task CreateCategoryCommandHandler_Failure()
    {
        // Arrange
        var categoryRepositoryMock = new Mock<ICategoryRepository>();
        var messageBusMock = new Mock<IMessageBusClient>();

        // Act
        var commandHandler = new CreateCategoryCommandHandler(categoryRepositoryMock.Object, messageBusMock.Object);

        var command = new CreateCategoryCommand
        {
            Name = "",
            Description = "",
            ShortDescription = ""
        };

        Func<Task> act = async () => await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();

        categoryRepositoryMock.Verify(pr => pr.Add(It.IsAny<Category>()), Times.Never);
    }
}
