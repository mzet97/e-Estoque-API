namespace e_Estoque_API.Core.Models
{
    public class Keycloak
    {
        public string Realm { get; set; } = string.Empty;
        public string AuthServerUrl { get; set; } = string.Empty;
        public string SslRequired { get; set; } = string.Empty;
        public string Resource { get; set; } = string.Empty;
        public string VerifyTokenAudience { get; set; } = string.Empty;
        public Credentials Credentials { get; set; }
        public string ConfidentialPort { get; set; } = string.Empty;
    }

    public class Credentials
    {
        public string Secret { get; set; } = string.Empty;
    }
}
