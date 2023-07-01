using Campany.BL.Dots;

namespace BlazorDay3.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        public Task<UserDetailsDto> GetUserDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TokenDto> LoginAsync(LoginDto credentials)
        {
            
        }
    }
}
