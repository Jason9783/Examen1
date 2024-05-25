
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using MyFavPokeMini.Models;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);




        // add Services
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
       


         var app = builder.Build();



        // Middlewares

          app.UseSwagger();
          app.UseSwaggerUI();
       





        app.MapGet("/response", async () =>
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://pokeapi.co/api/v2/pokemon/pikachu");
                 response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var pokemonData = JsonDocument.Parse(responseBody);

            var name = pokemonData.RootElement.GetProperty("name").GetString();

            var moves = new List<string>();
               foreach (var move in pokemonData.RootElement.GetProperty("moves").EnumerateArray())
               {
                   moves.Add(move.GetProperty("move").GetProperty("name").GetString());
               }

               var types = new List<string>();
                   foreach (var type in pokemonData.RootElement.GetProperty("types").EnumerateArray())
                     {
                    types.Add(type.GetProperty("type").GetProperty("name").GetString());
                   }

            
            var spriteUrl = pokemonData.RootElement.GetProperty("sprites").GetProperty("front_default").GetString();






            var pokemonResponse = new PokemonResponse
              {
                  Nombre = name,
                  Movimientos = moves,
                  Tipo = types,
                  SpriteUrl = spriteUrl
              };

            return Results.Json(pokemonResponse);
        });





        app.Run();
    }

   
}








