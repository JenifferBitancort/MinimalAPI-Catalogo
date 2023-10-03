using ApiCatalogo.Models;
using APICatalogo__MinimalApi_.Models;

namespace ApiCatalogo.Services;

public interface ITokenService
{
    string GerarToken(string key, string issuer, string audience, UserModel user);
}
