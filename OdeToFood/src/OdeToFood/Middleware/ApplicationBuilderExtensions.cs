using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// This is our custom middleware implementation to bring in all files under node_modules, and expose as virtual folder with same name
        /// </summary>
        public static IApplicationBuilder UseNodeModules(this IApplicationBuilder app, string root)
        {

            string nodeFolderName = "node_modules";

            string nodeFolderPath = Path.Combine(root, nodeFolderName);

            var nodeFileProvider = new PhysicalFileProvider(nodeFolderPath);

            var options = new StaticFileOptions
            {
                RequestPath = $"/{nodeFolderName}",
                FileProvider = nodeFileProvider
            };

            app.UseStaticFiles(options);

            return app;
        }
    }
}
