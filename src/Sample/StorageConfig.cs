namespace Zelda.TableStorage.Sample {
    public class StorageConfig {
        public string AccountName { get; set; }
        public string AccessKey { get; set; }

        public override string ToString() {
            return
                string.Format(
                    "DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", AccountName, AccessKey);
        }
    }
}