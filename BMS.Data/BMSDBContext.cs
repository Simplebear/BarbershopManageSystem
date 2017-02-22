using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Data
{
   // [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class BMSDBContext: DbContext, IDisposable
    {
        public BMSDBContext():base("DefaultConnection")
        {
            ConfigureDbContext();
        }
        public static BMSDBContext Create()
        {
            return new BMSDBContext();
        }
        public virtual void Commit()
        {
            base.SaveChanges();
        }
        private void ConfigureDbContext()
        {
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.UseDatabaseNullSemantics = true;
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
        }
        /// <summary>
        /// 创建对象映射到数据库
        /// </summary>
        /// <param name="modelBuilder">ORM数据映射对象</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            // modelBuilder.Entity<Company>().ToTable("Company", "public");
            //modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<User> User { get; set; }
    }
}
