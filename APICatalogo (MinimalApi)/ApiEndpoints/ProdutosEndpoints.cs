using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo__MinimalApi_.ApiEndpoints
{
    public static class ProdutosEndpoints
    {

        public static void MapProdutosEndPoints(this WebApplication app)
        {

            app.MapGet("/produtos", async (AppDbContext db) => await db.Produtos.ToListAsync());
           



            app.MapGet("/produtos/{id}", async (AppDbContext db, int id) =>
            {         

                return await db.Produtos.FindAsync(id)  
                  is Produto produto ? Results.Ok(produto) 
                  : Results.NotFound();   

            });

            app.MapPost("/produtos", async (AppDbContext db, Produto produto) =>
            {           
                db.Produtos.Add(produto); 
                await db.SaveChangesAsync(); 
                return Results.Created($"/produtos/{produto.ProdutoId}", produto);  
            });

            app.MapPut("/produtos/{id:int}", async (AppDbContext db, int id, Produto produto) =>
            {        

                if (produto.ProdutoId != id) 
                {
                    return Results.BadRequest(); 
                }

                var produtoDb = await db.Produtos.FindAsync(id); 

                if (produtoDb == null) 
                {
                    return Results.NotFound();
                }

                produtoDb.Nome = produto.Nome; 
                produtoDb.Descricao = produto.Descricao; 

                await db.SaveChangesAsync(); 
                return Results.Ok(produto); 
            });

            app.MapDelete("/produtos/{id:int}", async (AppDbContext db, int id) =>
            {          

                var produtoDb = await db.Produtos.FindAsync(id);  

                if (produtoDb == null) 
                {
                    return Results.NotFound();
                }

                db.Produtos.Remove(produtoDb);   
                await db.SaveChangesAsync(); 
                return Results.NoContent();
            });

        }
    }
}
