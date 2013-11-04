namespace Zelda.TableStorage {
    using System.Data.Services.Client;
    using System.Linq;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Table;
    using Microsoft.WindowsAzure.Storage.Table.DataServices;
    using Zelda;

    public class StorageRepository<TEntity> : RepositoryBase<TEntity>
        where TEntity : TableServiceEntity {

        public TableServiceContext GetContext() {
            return TableClient.GetTableServiceContext();
        }

        public CloudTableClient TableClient { get; private set; }

        public StorageRepository(CloudStorageAccount storageAccount) {
            TableClient = storageAccount.CreateCloudTableClient();

            TableClient
                .GetTableReference(EntitySet)
                .CreateIfNotExists();
        }

        public override void Add(TEntity entity) {
            using (var context = GetContext()) {
                context.AddObject(EntitySet, entity);
                context.SaveChanges();
            }
        }

        public override void Remove(TEntity entity) {
            using (var context = GetContext()) {
                context.AttachTo(EntitySet, entity, "*");
                context.DeleteObject(entity);
                context.SaveChangesWithRetries(SaveChangesOptions.Batch);
            }
        }

        public override void Update(TEntity entity) {
            using (var context = GetContext()) {
                context.AttachTo(EntitySet, entity, "*");
                context.UpdateObject(entity);
                context.SaveChangesWithRetries(SaveChangesOptions.ReplaceOnUpdate);
            }
        }

        public override IQueryable<TEntity> Linq() {
            var entitySet = EntitySet;
            var context = GetContext();
            return context.CreateQuery<TEntity>(entitySet);
        }

        public string EntitySet {
            get {
                var entityType = typeof(TEntity);
                var entityName = entityType.Name.Replace("Entity", string.Empty);
                var entitySet = entityName + "s";

                return entitySet;
            }
        }
    }
}
