using BMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Data
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class BMSDBContext: DbContext, IDisposable
    {
        public BMSDBContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Database.SetInitializer<BMSDBContext>(null);
            //ConfigureDbContext();
        }
        public BMSDBContext():base("name = DefaultConnection")
        {
            //ConfigureDbContext();
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
        public virtual DbSet<LoginHistory> LoginHistory { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Share> Share { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<Announcement> Announcement { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderPackage> OrderPackage { get; set; }
        public virtual DbSet<Package> Package { get; set; }
    }
}
