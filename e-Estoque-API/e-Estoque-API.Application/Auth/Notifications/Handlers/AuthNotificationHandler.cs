using e_Estoque_API.Application.Extensions;
using e_Estoque_API.Infrastructure.MessageBus;
using MediatR;
using Microsoft.Extensions.Logging;

namespace e_Estoque_API.Application.Auth.Notifications.Handlers
{
    public class AuthNotificationHandler :
                            INotificationHandler<LoginUserNotification>,
                            INotificationHandler<RegisterUserNotification>
    {
        private readonly IMessageBusClient _messageBus;
        private readonly ILogger<AuthNotificationHandler> _logger;

        public AuthNotificationHandler(IMessageBusClient messageBus, ILogger<AuthNotificationHandler> logger)
        {
            _messageBus = messageBus;
            _logger = logger;
        }

        public Task Handle(LoginUserNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                _logger.LogInformation("LoginUserNotification received: {Username}", notification.Username);
                var routingKey = notification.GetType().Name.ToDashCase();

                _messageBus.Publish(notification, routingKey, "noticiation-service");
            });
        }

        public Task Handle(RegisterUserNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                _logger.LogInformation("RegisterUserNotification received: " + notification.ToString(), notification.Username);
                var routingKey = notification.GetType().Name.ToDashCase();

                _messageBus.Publish(notification, routingKey, "noticiation-service");
            });
        }
    }
}
