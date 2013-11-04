namespace Zelda.TableStorage.Sample {
    using System;
    using System.Linq;
    using Zelda.TableStorage.Config;

    class Program {
        static void Main() {
            var person = new Person {
                Id = Guid.NewGuid().ToString(),
                LastName = "Test",
                FirstName = "User"
            };

            var entity = new PersonEntity(person);
            var repo = GetRepository();

            repo.Add(entity);

            var foundEntity = repo
                .Where(e => e.PartitionKey == person.LastName)
                .Where(e => e.RowKey == person.Id)
                .SingleOrDefault();

            Console.WriteLine("{0} {1}", foundEntity.FirstName, foundEntity.LastName);

            Console.Write("Press any key to exit...");
            Console.ReadKey(true);
        }

        public static IRepository<PersonEntity> GetRepository() {
            var storageAccount = StorageManager.GetStorageAccount();
            return new StorageRepository<PersonEntity>(storageAccount);
        }
    }
}
