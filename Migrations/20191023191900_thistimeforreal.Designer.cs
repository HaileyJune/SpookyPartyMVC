﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpookyPartyMVC.Models;

namespace SpookyPartyMVC.Migrations
{
    [DbContext(typeof(SpookyContext))]
    [Migration("20191023191900_thistimeforreal")]
    partial class thistimeforreal
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SpookyPartyMVC.Models.Catergory", b =>
                {
                    b.Property<int>("CatergoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CatergoryName");

                    b.HasKey("CatergoryId");

                    b.ToTable("Catergories");
                });

            modelBuilder.Entity("SpookyPartyMVC.Models.Entry", b =>
                {
                    b.Property<int>("EntryId");

                    b.Property<string>("CostumeName");

                    b.Property<string>("Description");

                    b.Property<int>("UserId");

                    b.HasKey("EntryId");

                    b.ToTable("Entries");
                });

            modelBuilder.Entity("SpookyPartyMVC.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EntryId");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SpookyPartyMVC.Models.Vote", b =>
                {
                    b.Property<int>("VoteId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CatergoryId");

                    b.Property<int>("EntryId");

                    b.Property<int>("UserId");

                    b.HasKey("VoteId");

                    b.HasIndex("CatergoryId");

                    b.HasIndex("EntryId");

                    b.HasIndex("UserId");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("SpookyPartyMVC.Models.Entry", b =>
                {
                    b.HasOne("SpookyPartyMVC.Models.User", "User")
                        .WithOne("Entry")
                        .HasForeignKey("SpookyPartyMVC.Models.Entry", "EntryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SpookyPartyMVC.Models.Vote", b =>
                {
                    b.HasOne("SpookyPartyMVC.Models.Catergory", "Catergory")
                        .WithMany("Votes")
                        .HasForeignKey("CatergoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SpookyPartyMVC.Models.Entry", "Entry")
                        .WithMany("Votes")
                        .HasForeignKey("EntryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SpookyPartyMVC.Models.User", "User")
                        .WithMany("Votes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
