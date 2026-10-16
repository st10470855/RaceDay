
using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace RaceDay.API.Authentication;

public class SessionAuthenticationHandler
    : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public SessionAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder)
        : base(options, logger, encoder)
    {
    }

    protected override Task<AuthenticateResult>
        HandleAuthenticateAsync()
    {
        var userId = Context.Session.GetInt32("UserID");
        var role = Context.Session.GetString("Role");

        if (userId == null || string.IsNullOrWhiteSpace(role))
        {
            return Task.FromResult(
                AuthenticateResult.NoResult());
        }

        var claims = new[]
        {
            new Claim(
                ClaimTypes.NameIdentifier,
                userId.Value.ToString()),

            new Claim(ClaimTypes.Role, role)
        };

        var identity = new ClaimsIdentity(
    claims,
    Scheme.Name,
    ClaimTypes.Name,
    ClaimTypes.Role);

        var principal = new ClaimsPrincipal(identity);

        var ticket = new AuthenticationTicket(
            principal, Scheme.Name);

        return Task.FromResult(
            AuthenticateResult.Success(ticket));
    }
}
