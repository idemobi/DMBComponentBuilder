#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBComponentBuilder.csproj ComponentBuilderConfigureOptions.cs create at 2026/04/07 21:04:27
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

#endregion

namespace DMBComponentBuilder
{
    public class ComponentBuilderConfigureOptions : IPostConfigureOptions<StaticFileOptions>
    {
        #region Constants

        const string K_BasePath = "wwwroot";

        #endregion

        #region Instance fields and properties

        private IWebHostEnvironment Environment { get; }

        #endregion

        #region Instance constructors and destructors

        public ComponentBuilderConfigureOptions(IWebHostEnvironment environment)
        {
            Environment = environment;
        }

        #endregion

        #region Instance methods

        #region From interface IPostConfigureOptions<StaticFileOptions>

        public void PostConfigure(string? name, StaticFileOptions options)
        {
            name = name ?? throw new ArgumentNullException(nameof(name));
            options = options ?? throw new ArgumentNullException(nameof(options));

            options.ContentTypeProvider = options.ContentTypeProvider ?? new FileExtensionContentTypeProvider();
            if (options.FileProvider == null && Environment.WebRootFileProvider == null)
            {
                throw new InvalidOperationException("Missing FileProvider.");
            }

            options.FileProvider = options.FileProvider ?? Environment.WebRootFileProvider;
            var tFilesProvider = new ManifestEmbeddedFileProvider(GetType().Assembly, K_BasePath);
            options.FileProvider = new CompositeFileProvider(options.FileProvider, tFilesProvider);
        }

        #endregion

        #endregion
    }
}