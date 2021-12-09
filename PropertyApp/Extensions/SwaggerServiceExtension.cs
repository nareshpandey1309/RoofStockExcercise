﻿using Microsoft.Extensions.DependencyInjection;


namespace PropertyAPI.Extensions
{
    public static class SwaggerServiceExtension
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, string version, string title, string description, string termsOfService, string authDescription)
        {
            // Register the Swagger services
            //services.AddSwaggerDocumentation(version, title, description, termsOfService, authDescription);
       
            return services;
        }

        //public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app, bool reverseProxy, string pathBase)
        //{
        //    app.UseSwaggerDocumentation(settings =>
        //    {
        //        settings.PostProcess = (document, request) =>
        //        {
        //            document.Schemes.Clear();
        //            document.Schemes.Add(reverseProxy ? SwaggerSchema.Https : SwaggerSchema.Http);
        //        };
        //    });

        //    app.UseSwaggerUi3(settings =>
        //    {
        //        settings.DefaultModelExpandDepth = 2;
        //        settings.CustomStylesheetUri = new Uri($"{pathBase}/assets/styles/swagger-ui.css", UriKind.Relative);
        //        settings.CustomJavaScriptUri = new Uri($"{pathBase}/assets/scripts/swagger-ui-standalone-preset.js", UriKind.Relative);
        //        settings.Path = "/api";
        //        settings.DocumentPath = $"{pathBase}/swagger/{{documentName}}/swagger.json";

        //    });
        //    return app;
        //}
    }
}
