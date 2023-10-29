using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using Sucre_DataAccess.Entities;
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

        //Alarm and fire automatic protection table
        public DbSet<AsPaz> AsPazs { get; set; }
        //measurement channel table
        public DbSet<Canal> Canals { get; set; }
        //table of metering point locations
        public DbSet<Cex> Cexs { get; set; }
        //table of energy types
        public DbSet<Energy> Energies { get; set; }
        //user group table
        public DbSet<GroupUser> GroupUsers { get; set; }
        //table of parameter types
        public DbSet<ParameterType> ParameterTypes { get; set; }
        //table of metering points
        public DbSet<Point> Points { get; set; }
        //table reports
        public DbSet<Report> Reports { get; set; }
        //table report details
        public DbSet<ReportDetail> ReportDetails { get; set; }
        //table users
        public DbSet<User> Users { get; set; }
        //daily values table
        public DbSet<ValueDay> ValuesDay { get; set; }
        //hourly value table
        public DbSet<ValueHour> ValuesHour { get; set; }
        //table of values by month
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

            //modelBuilder.Entity<ParameterType>().HasKey(x => x.Id);
            //modelBuilder.Entity<ParameterType>().Property(x => x.Id).ValueGeneratedOnAdd();

        }

    }
}
