using MediatR;
using System.Text.Json;

namespace e_Estoque_API.Application.Auth.Notifications;

public class RegisterUserNotification : INotification
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public override string? ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}