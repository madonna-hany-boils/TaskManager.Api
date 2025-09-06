
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Tasky.Services;

public class JwtOptions
{
    public string Issuer { get; set; } = "Tasky";
    public string Audience { get; set; } = "TaskyUsers";
    public string Key { get; set; } = default!;
}

public class JwtTokenService
{
    private readonly JwtOptions _opts;
    private readonly SymmetricSecurityKey _key;

    public JwtTokenService(IOptions<JwtOptions> opts)
    {
        _opts = opts.Value;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opts.Key));
    }

    public string CreateToken(int userId, string username, int expiresHours = 24)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, username)
        };

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _opts.Issuer,
            audience: _opts.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(expiresHours),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
