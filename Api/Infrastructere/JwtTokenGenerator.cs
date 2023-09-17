using Api.Core.Dto;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Infrastructere;

public class JwtTokenGenerator
{
    public TokenResponseDto GenerateToken(CheckUserDto dto)
    {
        List<Claim> Claims = new List<Claim>();
        Claims.Add(new Claim(ClaimTypes.Role, dto.Role));
        Claims.Add(new Claim(ClaimTypes.NameIdentifier, dto.Id.ToString()));
        Claims.Add(new Claim(ClaimTypes.Name, dto.UserName));
        SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Ferhatferhatferhat.1"));

        SigningCredentials SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
        issuer: "http://localhost",
        audience: "http://localhost",
        claims: Claims,
        notBefore:DateTime.UtcNow,
        expires:DateTime.Now.AddMinutes(1),
        signingCredentials:SigningCredentials

        );
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        string writedtoken = tokenHandler.WriteToken(token);
        return new TokenResponseDto(writedtoken, DateTime.Now.AddMinutes(1));
    }
}
