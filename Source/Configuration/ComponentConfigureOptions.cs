#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

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
    /// <summary>
    ///     Configures component builder configuration services and embedded assets for consuming applications.
    /// </summary>
    public class ComponentBuilderConfigureOptions : IPostConfigureOptions<StaticFileOptions>
    {
        #region Constants

        const string K_BasePath = "wwwroot";

        #endregion

        #region Instance fields and properties

        private IWebHostEnvironment Environment { get; }

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ComponentBuilderConfigureOptions" /> class.
        /// </summary>
        /// <param name="environment">The environment value.</param>
        public ComponentBuilderConfigureOptions(IWebHostEnvironment environment)
        {
            Environment = environment;
        }

        #endregion

        #region Instance methods

        #region From interface IPostConfigureOptions<StaticFileOptions>

        /// <summary>
        ///     Creates or renders the component builder configuration component through the post configure helper.
        /// </summary>
        /// <param name="name">The name value.</param>
        /// <param name="options">The options value.</param>
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