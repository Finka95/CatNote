using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CatNote.API;

public class AuthenticationConfiguration
{
    public string ClientSecret { get; set; }

    public SecurityKey GetSecurityKey() =>
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ClientSecret));
}
