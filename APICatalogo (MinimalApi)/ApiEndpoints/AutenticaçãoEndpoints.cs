using ApiCatalogo.Services;
using APICatalogo__MinimalApi_.Models;
using Microsoft.AspNetCore.Authorization;

namespace APICatalogo__MinimalApi_.ApiEndpoints
{
    public static class AutenticaçãoEndpoints
    {

        public static void MapAutenticacaoEndPoints(this WebApplication app) 
        {

            app.MapPost("/login", [AllowAnonymous] (UserModel userModel, ITokenService tokenService) =>
            {          

                if (userModel == null)  
                {
                    return Results.BadRequest("Login Inválido");
                }
                if (userModel.UserName == "Jeniffer" && userModel.Password == "jen123")  
                {
         
                    var tokenString = tokenService.GerarToken(app.Configuration["Jwt:Key"], 
                        app.Configuration["Jwt:Issuer"],
                        app.Configuration["Jwt:Audience"],
                        userModel);

                    return Results.Ok(new { token = tokenString }); 
                }
                else
                {
                    return Results.BadRequest("Login Inválido"); 
                }
            }).Produces(StatusCodes.Status400BadRequest)
                          .Produces(StatusCodes.Status200OK)
                          .WithName("Login")
                          .WithTags("Autenticacao");
        }



    }
}
