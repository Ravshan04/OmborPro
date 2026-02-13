using System.Threading.Tasks;
using OmborPro.Application.DTOs.Auth;

namespace OmborPro.Application.Common.Interfaces;

public interface IAuthService
{
    Task<AuthResponse?> LoginAsync(LoginRequest request);
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
}
