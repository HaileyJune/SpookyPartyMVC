    using Microsoft.EntityFrameworkCore; 
    namespace SpookyPartyMVC.Models
    {
        public class SpookyContext : DbContext
        {
            public SpookyContext(DbContextOptions options) : base(options) { }
            public DbSet<User> Users {get;set;}
            public DbSet<Entry> Entries {get;set;}
            public DbSet<Vote> Votes {get;set;}
            public DbSet<Catergory> Catergories {get;set;}

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<User>()
                    .HasOne(p => p.Entry)
                    .WithOne(i => i.User)
                    .HasForeignKey<Entry>(b => b.EntryId);
            }
        }
    }