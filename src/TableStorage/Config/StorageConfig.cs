namespace Zelda.TableStorage.Config {
    public class StorageConfig {
        public string AccountName { get; set; }
        public string AccessKey { get; set; }
        public bool UseHttps { get; set; }

        public override string ToString() {
            return
                string.Format(
                    "DefaultEndpointsProtocol={2};AccountName={0};AccountKey={1}",
                    AccountName,
                    AccessKey,
                    UseHttps ? "https" : "http");
        }
    }
}
