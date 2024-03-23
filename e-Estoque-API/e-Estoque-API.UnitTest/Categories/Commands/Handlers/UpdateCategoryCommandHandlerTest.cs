using e_Estoque_API.Application.Categories.Commands;
using e_Estoque_API.Application.Categories.Commands.Handlers;
using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Fakes;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Infrastructure.MessageBus;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace e_Estoque_API.UnitTest.Categories.Commands.Handlers
{
    public class UpdateCategoryCommandHandlerTest : BaseTest, IClassFixture<BaseTest>
    {
        private readonly ServiceProvider _serviceProvide;

        public UpdateCategoryCommandHandlerTest(BaseTest baseTest)
        {
            _serviceProvide = baseTest.ServiceProvider;
        }

        [Fact(DisplayName = "Test update category success")]
        [Trait("UpdateCategoryCommandHandlerTest", "UpdateCategory CommandHandler Tests")]
        public async Task UpdateCategoryCommandHandlerSuccess()
        {
            // Arrange
            var categoryRepository = _serviceProvide.GetService<ICategoryRepository>();
            var messageBusMock = new Mock<IMessageBusClient>();

            var listAll = await categoryRepository.GetAll();
            var category = listAll.First();

            // Act
            var commandHandler = new UpdateCategoryCommandHandler(categoryRepository, messageBusMock.Object);
            var command = new UpdateCategoryCommand(category.Id, "New Name", "New Short Description", "New Description");

            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            categoryRepository.Should().NotBeNull();
            result.Should().NotBeEmpty();
        }

        [Fact(DisplayName = "Test update category failure with not found")]
        [Trait("UpdateCategoryCommandHandlerTest", "UpdateCategory CommandHandler Tests")]
        public async Task UpdateCategoryCommandHandlerNotFoundFailure()
        {
            // Arrange
            var categoryRepository = _serviceProvide.GetService<ICategoryRepository>();
            var messageBusMock = new Mock<IMessageBusClient>();
            var faker = new GenerateCategoryFake().CreateInValid(1).First();

            // Act
            var commandHandler = new UpdateCategoryCommandHandler(categoryRepository, messageBusMock.Object);
            var command = new UpdateCategoryCommand(faker.Id, "New Name", "New Short Description", "New Description");

            Func<Task> act = async () => await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            categoryRepository.Should().NotBeNull();
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact(DisplayName = "Test update category failure with Validation")]
        [Trait("UpdateCategoryCommandHandlerTest", "UpdateCategory CommandHandler Tests")]
        public async Task UpdateCategoryCommandHandlerValidationFailure()
        {
            // Arrange
            var categoryRepository = _serviceProvide.GetService<ICategoryRepository>();
            var messageBusMock = new Mock<IMessageBusClient>();

            var listAll = await categoryRepository.GetAll();
            var category = listAll.First();

            // Act
            var commandHandler = new UpdateCategoryCommandHandler(categoryRepository, messageBusMock.Object);
            var command = new UpdateCategoryCommand(category.Id, "", "", "");

            Func<Task> act = async () => await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            categoryRepository.Should().NotBeNull();
            await act.Should().ThrowAsync<ValidationException>();
        }
    }
}
