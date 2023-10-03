using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo__MinimalApi_.ApiEndpoints
{
    public static class CategoriaEndpoints
    {

        public static void MapCategoriasEndPoints(this WebApplication app)
        {


            app.MapGet("/categorias", async (AppDbContext db) => await db.Categorias.ToListAsync()).RequireAuthorization();
           



            app.MapGet("/categorias/{id}", async (AppDbContext db, int id) =>
            {         

                return await db.Categorias.FindAsync(id)  
                is Categoria categoria ? Results.Ok(categoria)  
                : Results.NotFound();   

            });

            app.MapPost("/categorias", async (AppDbContext db, Categoria categoria) =>
            {           
                db.Categorias.Add(categoria); 
                await db.SaveChangesAsync();
                return Results.Created($"/categorias/{categoria.CategoriaId}", categoria);   
            });

            app.MapPut("/categorias/{id:int}", async (AppDbContext db, int id, Categoria categoria) =>
            {          

                if (categoria.CategoriaId != id) 
                {
                    return Results.BadRequest(); 
                }

                var categoriaDb = await db.Categorias.FindAsync(id); 

                if (categoriaDb == null) 
                {
                    return Results.NotFound();
                }

                categoriaDb.Nome = categoria.Nome;
                categoriaDb.Descricao = categoria.Descricao; 

                await db.SaveChangesAsync(); 
                return Results.Ok(categoria); 
            });

            app.MapDelete("/categorias/{id:int}", async (AppDbContext db, int id) =>
            {          

                var categoriaDb = await db.Categorias.FindAsync(id);  

                if (categoriaDb == null)
                {
                    return Results.NotFound();
                }

                db.Categorias.Remove(categoriaDb);   
                await db.SaveChangesAsync(); 
                return Results.NoContent();
            });



        }
    }
}