namespace Zelda.TableStorage.Sample {
    using System.Configuration;

    public static class ConfigManager {
        public static StorageConfig GetStorageConfig() {
            var appSettings = ConfigurationManager.AppSettings;

            var accountName = appSettings["storage.AccountName"];
            var accessKey = appSettings["storage.AccessKey"];

            return new StorageConfig { AccessKey = accessKey, AccountName = accountName };
        }
    }
}