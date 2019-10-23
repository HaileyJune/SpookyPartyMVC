using System.Collections.Generic;
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
            
            
            modelBuilder.Entity<Catergory>().HasData(
                new Catergory() { CatergoryId = 1, CatergoryName = "Scariest", Votes = new List<Vote>()},
                new Catergory() { CatergoryId = 2, CatergoryName = "Sexiest", Votes = new List<Vote>()},
                new Catergory() { CatergoryId = 3, CatergoryName = "Funniest", Votes = new List<Vote>()},
                new Catergory() { CatergoryId = 4, CatergoryName = "Best Couple", Votes = new List<Vote>()},
                new Catergory() { CatergoryId = 5, CatergoryName = "Honorable Mention", Votes = new List<Vote>()}

            );
            }

        }
    }