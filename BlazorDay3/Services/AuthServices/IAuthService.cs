using Campany.BL.Dots;

namespace BlazorDay3.Services.AuthServices;
public interface IAuthService
{
    Task<TokenDto> LoginAsync(LoginDto credentials);
    Task<UserDetailsDto> GetUserDetailsAsync();
}
