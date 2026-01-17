using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using RedeSocial.Application.Contracts.Documents.Dtos;

namespace RedeSocial.WebApi.Security;

public class TokenService
{
   public string GenerateToken(LoginDto login)
    {
        var SecretKey = Encoding.UTF8.GetBytes(TokenSettings.SecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(TokenSettings.ExpiresInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(SecretKey), SecurityAlgorithms.HmacSha256Signature),
            Subject = new ClaimsIdentity(new [] 
            {
                new Claim("Id", login.Id.ToString()),
                new Claim(ClaimTypes.Name, login.Name),
                new Claim(ClaimTypes.Email, login.Email)
            })
        };

        var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    } 
}
