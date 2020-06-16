using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Anjoz.Identity.Domain.Excecoes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;

namespace Anjoz.Identity.WebApi.Configuracoes
{
    public static class ExceptionHandlerConfiguration
    {
        public static void UseGlobalExceptions(this IApplicationBuilder app, IHostingEnvironment env,
            string policesDefaultAllowAll)
        {
            app.UseExceptionHandler(builder =>
                builder.UseCors(policesDefaultAllowAll)
                    .Run(async context => await ManipulateException(context, env)));
        }

        private static async Task ManipulateException(HttpContext context, IHostingEnvironment env)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;

            var errors = ListErrors(context, env);

            if (errors.Any())
            {
                var json = JsonConvert.SerializeObject(errors);
                await context.Response.WriteAsync(json);
            }
        }

        private static IEnumerable<string> ListErrors(HttpContext context, IHostingEnvironment env)
        {
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

            switch (exceptionHandlerPathFeature.Error)
            {
                case NotFoundException notFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    return notFoundException.Errors;

                case BusinessException businessException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    return businessException.Errors;

                default:
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    if (env.IsDevelopment())
                    {
                        return new[]
                        {
                            exceptionHandlerPathFeature.Error.Source,
                            exceptionHandlerPathFeature.Error.Message,
                            exceptionHandlerPathFeature.Error.StackTrace
                        };
                    }

                    return new[] {"Erro interno de servidor"};
                }
            }
        }
    }
}