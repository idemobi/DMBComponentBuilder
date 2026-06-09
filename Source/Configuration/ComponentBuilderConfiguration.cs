#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBServerWebHelper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    ///     Configures component builder configuration services and embedded assets for consuming applications.
    /// </summary>
    [Serializable]
    public class ComponentBuilderConfiguration : WebGenericConfiguration<ComponentBuilderConfiguration>, IServerWebConfig
    {
        #region Static constructors and destructors

        static ComponentBuilderConfiguration()
        {
        }

        #endregion

        #region Instance methods

        #region From interface IServerWebConfig

        /// <inheritdoc />
        public override void AfterConfiguration(IHostApplicationBuilder appBuilder, IConfigurationBuilder configBuilder, IConfigurationRoot configRoot)
        {
            appBuilder.Services.ConfigureOptions<ComponentBuilderConfigureOptions>();
        }

        /// <inheritdoc />
        public override bool ApiDescription()
        {
            return false;
        }

        /// <inheritdoc />
        public override void BeforeConfiguration(IHostApplicationBuilder appBuilder, IConfigurationBuilder configBuilder, IConfigurationRoot configRoot)
        {
        }

        /// <inheritdoc />
        public override bool NeedsConfigFileOrAppSettings()
        {
            return false;
        }

        /// <inheritdoc />
        public override void RandomFake()
        {
        }

        #endregion

        #endregion
    }
}