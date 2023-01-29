using CommissioningManager.Models;
using System.Data.Entity;

namespace CommissioningManager.Data
{
    public class DataContext<T> : DbContext where T : class
    {
        public DataContext() : base("name=PIMSDB_ZIUTEntities")
        {
            //
        }
        public DataContext(string databaseConnection) : base(databaseConnection)
        {
            //
        }

        public DbSet<T> Datas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
