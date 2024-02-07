namespace e_Estoque_API.Application.Auth.ViewModels;

public class UserDto
{
    public Attributes attributes { get; set; } = new Attributes();
    public Credential[] credentials { get; set; } = new Credential[1];
    public string username { get; set; } = string.Empty;
    public string firstName { get; set; } = string.Empty;
    public string lastName { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public bool emailVerified { get; set; }
    public bool enabled { get; set; }
}

public class Attributes
{
    public string attribute_key { get; set; } = string.Empty;
}

public class Credential
{
    public bool temporary { get; set; }
    public string type { get; set; } = string.Empty;
    public string value { get; set; } = string.Empty;
}