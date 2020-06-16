using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Anjoz.Identity.WebApi.Configuracoes
{
    public static class JwtConfiguration
    {
        private const string ChaveSectionw = "JwtConfiguration:Chave";
        
        public static void RegistrarAutenticacao(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection(ChaveSectionw).Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        public static void ConfigurarAutenticacao(this IApplicationBuilder app)
        {
            app.UseAuthentication();
        }
    }
}