using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Anjoz.Identity.WebApi.Configuracoes
{
    public static class SwaggerConfiguration
    {
        public static void RegistrarSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {Title = "Anjoz Identity", Version = "v1"});
                c.AddSecurityDefinition("Bearer",
                    new ApiKeyScheme
                    {
                        In = "header",
                        Description = "Copie 'Bearer ' + token'",
                        Name = "Authorization",
                        Type = "apiKey"
                    });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {{"Bearer", new string[] { }}});
            });
        }

        public static void ConfigurarSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Anjoz Identity v1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}