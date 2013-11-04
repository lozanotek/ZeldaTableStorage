namespace Zelda.TableStorage.Config {
    using System.Configuration;

    public static class ConfigParser {
        public static StorageConfig GetStorageConfig(string prefix = null) {

            var appSettings = ConfigurationManager.AppSettings;

            var format = string.IsNullOrEmpty(prefix) ? "." : ".{0}.";

            var nameConfigKey = string.Format("storage" + format + "AccountName", prefix);
            var accessConfigKey = string.Format("storage" + format + "AccessKey", prefix);
            var httpsConfigKey = string.Format("storage" + format + "UseHttps", prefix);

            var accountName = appSettings[nameConfigKey];
            var accessKey = appSettings[accessConfigKey];
            var useHttps = appSettings[httpsConfigKey];

            return new StorageConfig {
                AccessKey = accessKey,
                AccountName = accountName,
                UseHttps = ShouldUseHttps(useHttps)
            };
        }

        private static bool ShouldUseHttps(string value) {
            if (string.IsNullOrEmpty(value)) {
                // If not specified, make it so to avoid security breach
                return true;
            }

            bool useHttps;
            if (!bool.TryParse(value, out useHttps)) {
                useHttps = false;
            }

            return useHttps;
        }
    }
}