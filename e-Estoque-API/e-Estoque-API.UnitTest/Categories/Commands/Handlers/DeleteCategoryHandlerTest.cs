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
    public class DeleteCategoryHandlerTest : BaseTest, IClassFixture<BaseTest>
    {
        private readonly ServiceProvider _serviceProvide;

        public DeleteCategoryHandlerTest(BaseTest baseTest)
        {
            _serviceProvide = baseTest.ServiceProvider;
        }

        [Fact(DisplayName = "Test delete category success")]
        [Trait("DeleteCategoryHandlerTest", "DeleteCategory CommandHandler Tests")]
        public async Task DeleteCategoryHandlerSuccess()
        {
            // Arrange
            var categoryRepository = _serviceProvide.GetService<ICategoryRepository>();
            var messageBusMock = new Mock<IMessageBusClient>();
            var faker = new GenerateCategoryFake().CreateValid(1).First();
            var category = new Category(faker.Id, faker.Name, faker.Description, faker.ShortDescription);

            await categoryRepository.Add(category);

            // Act
            var commandHandler = new DeleteCategoryHandler(categoryRepository, messageBusMock.Object);
            var command = new DeleteCategoryCommand(category.Id);

            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            categoryRepository.Should().NotBeNull();
            result.Should().NotBeNull();
        }

        [Fact(DisplayName = "Test delete category failure")]
        [Trait("DeleteCategoryHandlerTest", "DeleteCategory CommandHandler Tests")]
        public async Task DeleteCategoryHandlerFailure()
        {
            // Arrange
            var categoryRepository = _serviceProvide.GetService<ICategoryRepository>();
            var messageBusMock = new Mock<IMessageBusClient>();
            var faker = new GenerateCategoryFake().CreateInValid(1).First();

            // Act
            var commandHandler = new DeleteCategoryHandler(categoryRepository, messageBusMock.Object);
            var command = new DeleteCategoryCommand(faker.Id);

            Func<Task> act = async () => await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            categoryRepository.Should().NotBeNull();
            await act.Should().ThrowAsync<NotFoundException>();
        }
    }
}
