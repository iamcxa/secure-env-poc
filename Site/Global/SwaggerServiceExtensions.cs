using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NSwag;

namespace Demo.Global
{
    public static class SwaggerServiceExtensions
    {

        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Cocos連線用Demo後端";
                    document.Info.Description = "開發者用API清單一覽";
                    document.Info.TermsOfService = "";
                    document.Info.License = new OpenApiLicense() { Name = "copyright © . all rights reserved" };
                    document.Info.Contact = new OpenApiContact
                    {
                        Name = "",
                        Email = "",
                        Url = "",
                    };
                };


                //config.AddSecurity("Basic", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                //{
                //    Type = OpenApiSecuritySchemeType.ApiKey,
                //    Description = "Add Header (Authorization : Basic {...})\r\n Be sure copy 'Basic' + your apiToken",

                //    In = OpenApiSecurityApiKeyLocation.Header,
                //    Scheme = "Basic",
                //    Name = "Authorization",
                //});
                //config.OperationProcessors.Add(new MyOperationProcessor("Basic"));
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            //app.UseSwagger(
            //    config =>
            //    {
            //        config.PostProcess = (doc, _) => doc.Schemes = new[] { OpenApiSchema.Https };
            //    }
            //);
            app.UseOpenApi();
            app.UseSwaggerUi3();
            return app;
        }
    }
}
