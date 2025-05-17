using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;

public class KeycloakClaimsTransformation : IClaimsTransformation
{
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var identity = (ClaimsIdentity)principal.Identity;

        var realmAccessClaim = identity.FindFirst("realm_access");
        if (realmAccessClaim != null)
        {
            try
            {
                var realmAccess = JsonDocument.Parse(realmAccessClaim.Value).RootElement;

                if (realmAccess.TryGetProperty("roles", out var rolesElement))
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
            catch
            {
                // Se houver problema na desserialização, ignore e siga
            }
        }

        return Task.FromResult(principal);
    }
}