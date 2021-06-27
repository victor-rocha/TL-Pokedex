using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Pokedex.Domain;
using Pokedex.Queries;
using Pokedex.Queries.Interfaces;
using Pokedex.Queries.Interfaces.Externals;
using Refit;

namespace Pokedex
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<BasicPokemonFetcher>();
            services.AddTransient<IPokemonQuery, PokemonQuery>();
            services.AddRefitClient<IPokeApiClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://pokeapi.co/api"));
            
            // A singleton cache service would be added here. 
            
            services.AddControllers();
            services.AddSwaggerGen(
                c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Pokedex.API", Version = "v1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pokedex v1"));

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}