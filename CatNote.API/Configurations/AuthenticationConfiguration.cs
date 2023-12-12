using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CatNote.API.Configurations;

public class AuthenticationConfiguration
{
    public string Domain { get; set; } = null!;

    public string Audience { get; set; } = null!;

    public string ClientSecret { get; set; } = null!;

    public SecurityKey GetSecurityKey() =>
        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ClientSecret));
}
