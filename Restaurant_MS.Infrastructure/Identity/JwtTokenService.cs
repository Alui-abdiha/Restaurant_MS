using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Restaurant_MS.API.Settings;
using Restaurant_MS.Infrastructure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Restaurant_MS.Infrastructure.Identity
{
    public class JwtTokenService
    {
        private readonly JwtSettings _settings;

        public JwtTokenService(IOptions<JwtSettings> options)
        {
            _settings = options.Value;
        }

        public (string Token, DateTime ExpiresAt) GenerateToken(
            ApplicationUser user,
            IEnumerable<string> roles,
            IEnumerable<string> permissions)
        {
            var expiresAt = DateTime.UtcNow.AddMinutes(
                _settings.ExpiryMinutes);

            var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, user.UserName ?? ""),
            new(JwtRegisteredClaimNames.Email, user.Email ?? ""),
            new("fullName", user.FullName)
        };

            claims.AddRange(roles.Select(role =>
                new Claim(ClaimTypes.Role, role)));

            claims.AddRange(permissions.Distinct().Select(permission =>
                new Claim("permission", permission)));

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_settings.Secret));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials);

            return (
                new JwtSecurityTokenHandler().WriteToken(token),
                expiresAt
            );
        }
    }
}
