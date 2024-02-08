using e_Estoque_API.Application.Extensions;
using e_Estoque_API.Infrastructure.MessageBus;
using MediatR;
using Microsoft.Extensions.Logging;

namespace e_Estoque_API.Application.Common.Notifications.Handlers
{
    public class ErrorNotificationHandler : INotificationHandler<ErrorNotification>
    {
        private readonly IMessageBusClient _messageBus;
        private readonly ILogger<ErrorNotificationHandler> _logger;

        public ErrorNotificationHandler(
            IMessageBusClient messageBus,
            ILogger<ErrorNotificationHandler> logger)
        {
            _messageBus = messageBus;
            _logger = logger;
        }

        public Task Handle(ErrorNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                _logger.LogError("ErrorNotification received: " + notification.Message);
                var routingKey = notification.GetType().Name.ToDashCase();

                _messageBus.Publish(notification, routingKey, "noticiation-service");
            });
        }
    }
}
