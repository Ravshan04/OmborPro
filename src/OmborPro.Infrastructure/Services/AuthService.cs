using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OmborPro.Application.Common.Interfaces;
using OmborPro.Application.DTOs.Auth;
using OmborPro.Domain.Entities;
using BC = BCrypt.Net.BCrypt;

namespace OmborPro.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IRepository<User> _userRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IRepository<User> userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<AuthResponse?> LoginAsync(LoginRequest request)
    {
        var users = await _userRepository.FindAsync(u => u.Email == request.Email);
        var user = users.FirstOrDefault();

        if (user == null || !BC.Verify(request.Password, user.PasswordHash))
            return null;

        var token = GenerateJwtToken(user);

        return new AuthResponse(
            token,
            user.Email,
            user.FirstName,
            user.LastName,
            user.Phone,
            user.AvatarUrl,
            user.OrganizationId
        );
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var user = new User
        {
            Email = request.Email,
            PasswordHash = BC.HashPassword(request.Password),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Phone = request.Phone,
            OrganizationId = request.OrganizationId ?? Guid.Empty,
            Roles = new List<string> { "User" }
        };

        await _userRepository.AddAsync(user);

        var token = GenerateJwtToken(user);

        return new AuthResponse(
            token,
            user.Email,
            user.FirstName,
            user.LastName,
            user.Phone,
            user.AvatarUrl,
            user.OrganizationId
        );
    }

    private string GenerateJwtToken(User user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Key"] ?? "a-very-long-secret-key-for-stock-harmony-42");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("OrganizationId", user.OrganizationId.ToString()),
                new Claim(ClaimTypes.Role, string.Join(",", user.Roles))
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}

