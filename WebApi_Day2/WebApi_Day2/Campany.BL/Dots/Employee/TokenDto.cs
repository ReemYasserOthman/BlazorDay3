namespace Campany.BL.Dots;
public enum TokenResult
{
    success,
    wrong_user_name,
    wrong_password,
    general_error
}

public class TokenDto
{
    public TokenDto(TokenResult result, string token = "", DateTime? expiry = null)
    {
        Token = token;
        Expiry = expiry ?? DateTime.Now; //??=if null

        Result = result;
    }
    public TokenDto()
    {

    }
    public string Token { get; set; } = string.Empty;
    public DateTime Expiry { get; set; }
    public TokenResult Result { get; set; }

}
