using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using API.Application.Results;
using API.Data;
using API.Data.Entities;
using API.Data.ValueObjects;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Application.Commands;

public class LoginUserHandler : BaseHandler<User>, IRequestHandler<LoginUserCommand, LoginResponseDto>
{
    public readonly IConfiguration _configuration;
    public LoginUserHandler(IMapper mapper, AppDbContext db, IConfiguration configuration)
            : base(mapper, db)
    {
        _configuration = configuration;
    }

    public async Task<LoginResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var targetUser = (await _db.Users.SingleOrDefaultAsync(u => u.Username == request.Username));

        if (targetUser == null || !Password.VerifyPasswordHash(request.Password, targetUser.PasswordHash, targetUser.PasswordSalt))
            throw new HttpRequestException("Wrong credential", null, HttpStatusCode.Unauthorized);


        return new LoginResponseDto() { Token = CreateToken(targetUser) };
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