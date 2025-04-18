using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;

public class KeycloakClaimsTransformation : IClaimsTransformation
{
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        // Copia a identidade atual
        var identity = (ClaimsIdentity)principal.Identity;

        // Extrai o claim com o json completo (caso exista) com as roles do resource_access
        var resourceAccessClaim = identity.FindFirst("resource_access");
        if (resourceAccessClaim != null)
        {
            try
            {
                // Desserializa o JSON do resource_access
                var resourceAccess = JsonDocument.Parse(resourceAccessClaim.Value).RootElement;

                if (resourceAccess.TryGetProperty("e-estoque-client", out var clientAccess))
                {
                    if (clientAccess.TryGetProperty("roles", out var rolesElement))
                    {
                        if (rolesElement.ValueKind == JsonValueKind.Array)
                        {
                            foreach (var role in rolesElement.EnumerateArray())
                            {
                                var roleValue = role.GetString();
                                if (!identity.Claims.Any(c => c.Type == identity.RoleClaimType && c.Value == roleValue))
                                {
                                    identity.AddClaim(new Claim(identity.RoleClaimType, roleValue));
                                }
                            }
                        }
                        else if (rolesElement.ValueKind == JsonValueKind.String)
                        {
                            var roleValue = rolesElement.GetString();
                            if (!identity.Claims.Any(c => c.Type == identity.RoleClaimType && c.Value == roleValue))
                            {
                                identity.AddClaim(new Claim(identity.RoleClaimType, roleValue));
                            }
                        }
                    }
                }
            }
            catch
            {
                // Se houver problema na desserialização, ignore e siga
            }
        }
        return Task.FromResult(principal);
    }
}
