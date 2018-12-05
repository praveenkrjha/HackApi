using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace JDA.Common
{
    /// <summary>
    /// JDA.Common.LegacyConfigurationProvider class for legacy configuration provider
    /// </summary>
    /// <seealso cref="Microsoft.Extensions.Configuration.ConfigurationProvider" />
    /// <seealso cref="Microsoft.Extensions.Configuration.IConfigurationSource" />
    public class LegacyConfigurationProvider : ConfigurationProvider, IConfigurationSource
    {
        /// <summary>
        /// The hosting environment
        /// </summary>
        private readonly IHostingEnvironment _hostingEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="LegacyConfigurationProvider"/> class.
        /// </summary>
        /// <param name="hostingEnvironment">The hosting environment.</param>
        public LegacyConfigurationProvider(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Loads (or reloads) the data for this provider.
        /// </summary>
        public override void Load()
        {
            //Run this method only on Windows. We would always use Windows server but, just for the sake of fun, allow support for other platforms
            bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            if (!isWindows) { return; }

            //Read appsettings.json manually and get the path of machine.config file.
            //We cannot use IOptions here because we are going to set to IOptions :)
            var appsettingsFilePath = Path.Combine(_hostingEnvironment.ContentRootPath, "appsettings.json");
            dynamic obj = JObject.Parse(File.ReadAllText(appsettingsFilePath));
            string machineConfigPath = obj.appSettings.MachineConfigPath.Value.ToString();

            //Load the machine.config file as XML and get the connections strings.
            var xDoc = XDocument.Load(machineConfigPath);
            //var xDoc = XDocument.Load(@"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\Config\machine.config");
            var connection = xDoc.Root.Elements("connectionStrings").Elements();
            foreach (var connectionString in connection)
            {
                var name = (string)connectionString.Attribute("name");
                var conStr = (string)connectionString.Attribute("connectionString");
                Data.Add($"ConnectionStrings:{name}", conStr);
            }
        }

        /// <summary>
        /// Builds the <see cref="T:Microsoft.Extensions.Configuration.IConfigurationProvider" /> for this source.
        /// </summary>
        /// <param name="builder">The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" />.</param>
        /// <returns>
        /// An <see cref="T:Microsoft.Extensions.Configuration.IConfigurationProvider" />
        /// </returns>
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return this;
        }
    }
}
