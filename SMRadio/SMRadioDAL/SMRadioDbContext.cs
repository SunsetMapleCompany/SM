using SM.Radio.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;

namespace SM.Radio.DAL
{
    public class SMRadioDbContext : DbContext
    {
        public SMRadioDbContext()
            : base("FMRadioConnection")
        { 

        }

        public DbSet<Radios> Radios { get; set; }

        public DbSet<Program> Programs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
