namespace Zelda.TableStorage {
    using System.Linq;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Table;
    using Zelda;

    public class StorageRepository<TEntity> : RepositoryBase<TEntity>
        where TEntity : TableEntity, new() {

        public CloudTableClient TableClient { get; private set; }
        public CloudTable Table { get; private set; }

        public StorageRepository(CloudStorageAccount storageAccount) {
            TableClient = storageAccount.CreateCloudTableClient();
            Table = TableClient.GetTableReference(typeof(TEntity).Name);
            Table.CreateIfNotExists();
        }

        public override void Add(TEntity entity) {
            var insertOperation = TableOperation.Insert(entity);
            Table.Execute(insertOperation);
        }

        public override void Remove(TEntity entity) {
            var tempEntity = new DynamicTableEntity(entity.PartitionKey, entity.RowKey) { ETag = "*" };
            var removeOperation = TableOperation.Delete(tempEntity);
            Table.Execute(removeOperation);
        }

        public override void Update(TEntity entity) {
            var updateOperation = TableOperation.Replace(entity);
            Table.Execute(updateOperation);
        }

        public override IQueryable<TEntity> Linq() {
            return Table.CreateQuery<TEntity>();
        }
    }
}
