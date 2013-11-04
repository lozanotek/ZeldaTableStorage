namespace Zelda.TableStorage.Config {
    using Microsoft.WindowsAzure.Storage;

    public static class StorageManager {
        public static CloudStorageAccount GetStorageAccount(string prefix = null) {
            var config = ConfigParser.GetStorageConfig(prefix);
            return CloudStorageAccount.Parse(config.ToString());
        }
    }
}
