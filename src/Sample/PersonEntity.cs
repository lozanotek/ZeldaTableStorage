namespace Zelda.TableStorage.Sample {
    using Microsoft.WindowsAzure.Storage.Table.DataServices;

    public class PersonEntity : TableServiceEntity {
        public PersonEntity() {
        }

        public PersonEntity(Person p) {
            PartitionKey = p.LastName;
            RowKey = p.Id;

            Id = p.Id;
            FirstName = p.FirstName;
            LastName = p.LastName;
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}