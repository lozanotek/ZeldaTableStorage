namespace Zelda.TableStorage.Sample {
    using Microsoft.WindowsAzure.Storage;

    public static class StorageManager {
        public static CloudStorageAccount GetStorageAccount() {
            var config = ConfigManager.GetStorageConfig();
            return CloudStorageAccount.Parse(config.ToString());
        }
    }
}