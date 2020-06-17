using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSMS.Models
{
    public class OSMSContext : DbContext
    {
        public OSMSContext(DbContextOptions<OSMSContext> options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Standard> Standards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                        .Property(s => s.RowVersion)
                        .IsRowVersion();
            modelBuilder.Entity<Instructor>()
                        .Property(s => s.RowVersion)
                        .IsRowVersion();
        }
    }
}
