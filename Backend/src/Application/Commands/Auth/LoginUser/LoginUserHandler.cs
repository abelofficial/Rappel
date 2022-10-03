using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using API.Application.Results;
using API.Domain.Entities;
using API.Domain.ValueObjects;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Application.Commands;

public class LoginUserHandler : BaseHandler<User>, IRequestHandler<LoginUserCommand, LoginResponseDto>
{
    public readonly IConfiguration _configuration;
    public LoginUserHandler(IMapper mapper, IRepository<User> db, IConfiguration configuration)
            : base(mapper, db)
    {
        _configuration = configuration;
    }

    public async Task<LoginResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var targetUser = (await _db.GetOne(u => u.Username == request.Username));
            if (!Password.VerifyPasswordHash(request.Password, targetUser.PasswordHash, targetUser.PasswordSalt))
                throw new Exception();

            return new LoginResponseDto() { Token = CreateToken(targetUser) };
        }
        catch (Exception)
        {
            throw new HttpRequestException("Wrong credential", null, HttpStatusCode.Unauthorized);
        }
    }

    public string CreateToken(User modelUser)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = new List<Claim>
            {
                new Claim("Id", modelUser.Id.ToString()),
                new Claim(ClaimTypes.Name, modelUser.Username),
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration["JWT:secret"]));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = credentials
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}