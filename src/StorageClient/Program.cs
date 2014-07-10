namespace StorageClient {
    using System;
    using System.Linq;

    using Microsoft.WindowsAzure.Storage.Table;
    using Zelda;
    using Zelda.TableStorage;
    using Zelda.TableStorage.Config;

    class Program {
        static void Main(string[] args) {
            var storageAccount = StorageManager.GetStorageAccount();
            var repo = new StorageRepository<LogEntry>(storageAccount);

            AddTest(repo);
            DeleteTest(repo);
            UpdateTest(repo);

            Console.Write("Press any key to exit...");
            Console.ReadKey(true);
        }

        private static void UpdateTest(IRepository<LogEntry> repo) {
            var temp = new LogEntry(DateTime.Now, Guid.NewGuid());
            repo.Add(temp);

            var newId = Guid.NewGuid();
            temp.Id = newId;

            repo.Update(temp);

            var found = repo
                .Where(t => t.PartitionKey == temp.PartitionKey)
                .Where(t => t.Id == temp.Id)
                .FirstOrDefault();

            if (found != null) {
                Console.WriteLine("Record '{0}' has been updated!", temp.RowKey);
            }
        }

        private static void DeleteTest(IRepository<LogEntry> repo) {
            var temp = new LogEntry(DateTime.Now, Guid.NewGuid());

            repo.Add(temp);
            Console.WriteLine("RowKey: {0}", temp.RowKey);

            repo.Remove(temp);
            var found = repo
                .Where(t => t.PartitionKey == temp.PartitionKey)
                .Where(t => t.RowKey == temp.RowKey)
                .FirstOrDefault();

            if (found == null) {
                Console.WriteLine("Record '{0}' has been deleted!", temp.RowKey);
            }
        }

        private static void AddTest(IRepository<LogEntry> repo) {
            repo.Add(new LogEntry(DateTime.Now, Guid.NewGuid()));
            repo.Add(new LogEntry(DateTime.Now, Guid.NewGuid()));
            repo.Add(new LogEntry(DateTime.Now, Guid.NewGuid()));

            var items = repo.ToList();
            foreach (var item in items) {
                Console.WriteLine(item.RowKey);
            }
        }
    }

    public class LogEntry : TableEntity {
        public LogEntry() {
        }

        public LogEntry(DateTime eventDate, Guid id) {
            PartitionKey = eventDate.Date.ToString("yyyyMMdd");
            RowKey = id.ToString();
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
