using Microsoft.EntityFrameworkCore;
using Sucre_DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sucre_DataAccess.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        public DbSet<AsPaz> AsPazs { get; set; }
        public DbSet<Canal> Canals { get; set; }
        public DbSet<Cex> Cexs { get; set; }
        public DbSet<Energy> Energies { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
        public DbSet<ParameterType> ParameterTypes { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportDetail> ReportDetails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ValueDay> ValuesDay { get; set; }
        public DbSet<ValueHour> ValuesHour { get; set; }
        public DbSet<ValueMounth> ValuesMounth { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ValueDay>().HasKey(key => new { key.Id, key.Date });
            modelBuilder.Entity<ValueHour>().HasKey(key => new { key.Id, key.Date, key.Hour });
            modelBuilder.Entity<ValueMounth>().HasKey(key => new { key.Id, key.Date });
            modelBuilder.Entity<ReportDetail>().HasNoKey();
            modelBuilder.Entity<Canal>().Ignore(field => field.ReportDetails);
            modelBuilder.Entity<Point>().Ignore(field => field.ReportDetails);
            modelBuilder.Entity<Report>().Ignore(field => field.ReportDetails);
        }

    }
}
