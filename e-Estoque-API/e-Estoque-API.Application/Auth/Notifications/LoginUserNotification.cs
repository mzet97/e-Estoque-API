using MediatR;
using System.Text.Json;

namespace e_Estoque_API.Application.Auth.Notifications;

public class LoginUserNotification : INotification
{
    public string Username { get; set; } = string.Empty;

    public override string? ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}