using Campany.BL.Dots;
using Campany.DAL.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Campany.BL.Managers;

public class AccManager : IAccManager
{

    private readonly IConfiguration _configuration;
    private readonly UserManager<Employee> _userManager;

    public AccManager(IConfiguration configuration,
        UserManager<Employee> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    public async Task<TokenDto> Login(LoginDto loginDto)
    {
        Employee? user = await _userManager.FindByNameAsync(loginDto.UserName);
        if (user == null)
        {

            //return BadRequest();
            return new TokenDto(TokenResult.wrong_user_name);
        }

        bool isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!isPasswordCorrect)
        {

            return new TokenDto(TokenResult.wrong_password);
        }

        var claimsList = await _userManager.GetClaimsAsync(user);

        return GenerateToken(claimsList);
    }

   
    public async Task<RegisterResultDto> Register(RegisterDto registerDto,string role)
    {
        var newEmployee = new Employee
        {
            UserName = registerDto.UserName,
            Email = registerDto.Email,
            Department = registerDto.Department,
        };

        var creationResult = await _userManager.CreateAsync(newEmployee,
            registerDto.Password);
        if (!creationResult.Succeeded)
        {
            return new RegisterResultDto(false,creationResult.Errors);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, newEmployee.Id),
            new Claim(ClaimTypes.Role, role),
            new Claim("Nationality", "Egyptian")
        };

        await _userManager.AddClaimsAsync(newEmployee, claims);

        return new RegisterResultDto(true);
    }
    #region token
    private TokenDto GenerateToken(IList<Claim> claimsList)
    {
        string keyString = _configuration.GetValue<string>("SecretKey") ?? string.Empty;
        var keyInBytes = Encoding.ASCII.GetBytes(keyString);
        var key = new SymmetricSecurityKey(keyInBytes);

        //Combination of secret Key and HashingAlgorithm
        var signingCredentials = new SigningCredentials(key,
            SecurityAlgorithms.HmacSha256Signature);

        //Putting All together
        var expiry = DateTime.Now.AddMinutes(15);

        var jwt = new JwtSecurityToken(
                expires: expiry,
                claims: claimsList,
                signingCredentials: signingCredentials);

        //Getting Token String
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenString = tokenHandler.WriteToken(jwt);

        return new TokenDto
        (TokenResult.success,
             tokenString,
             expiry
        );
    }
    #endregion






}
