using MediatR;

namespace e_Estoque_API.Application.Common.Notifications
{
    public class ErrorNotification : INotification
    {
        public string Message { get; set; } = string.Empty;
        public string StackTrace { get; set; } = string.Empty;
    }
}
