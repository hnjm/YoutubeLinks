﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YoutubeLinks.Api.Data.Database;

#nullable disable

namespace YoutubeLinks.Api.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("YoutubeLinks.Api.Data.Entities.Link", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Downloaded")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<int>("PlaylistId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PlaylistId");

                    b.ToTable("Links");
                });

            modelBuilder.Entity("YoutubeLinks.Api.Data.Entities.Playlist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Public")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Playlists");
                });

            modelBuilder.Entity("YoutubeLinks.Api.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EmailConfirmationToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("UserName")
                        .IsUnique()
                        .HasFilter("[UserName] IS NOT NULL");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTime(2024, 3, 4, 20, 13, 9, 633, DateTimeKind.Utc).AddTicks(2940),
                            Email = "freakfightsfan@gmail.com",
                            EmailConfirmed = true,
                            IsAdmin = true,
                            Modified = new DateTime(2024, 3, 4, 20, 13, 9, 633, DateTimeKind.Utc).AddTicks(2940),
                            Password = "AQAAAAIAAYagAAAAECWFTp9uY78qPzaRu0d3uaJNo3WOlRpwCuCyDLH+yg/TowsjzlMGxMurTnvyZaYSxA==",
                            UserName = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Created = new DateTime(2024, 3, 4, 20, 13, 9, 633, DateTimeKind.Utc).AddTicks(2940),
                            Email = "freakfightsfan1@gmail.com",
                            EmailConfirmed = true,
                            IsAdmin = false,
                            Modified = new DateTime(2024, 3, 4, 20, 13, 9, 633, DateTimeKind.Utc).AddTicks(2940),
                            Password = "AQAAAAIAAYagAAAAECWFTp9uY78qPzaRu0d3uaJNo3WOlRpwCuCyDLH+yg/TowsjzlMGxMurTnvyZaYSxA==",
                            UserName = "User"
                        });
                });

            modelBuilder.Entity("YoutubeLinks.Api.Data.Entities.Link", b =>
                {
                    b.HasOne("YoutubeLinks.Api.Data.Entities.Playlist", "Playlist")
                        .WithMany("Links")
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Playlist");
                });

            modelBuilder.Entity("YoutubeLinks.Api.Data.Entities.Playlist", b =>
                {
                    b.HasOne("YoutubeLinks.Api.Data.Entities.User", "User")
                        .WithMany("Playlists")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("YoutubeLinks.Api.Data.Entities.Playlist", b =>
                {
                    b.Navigation("Links");
                });

            modelBuilder.Entity("YoutubeLinks.Api.Data.Entities.User", b =>
                {
                    b.Navigation("Playlists");
                });
#pragma warning restore 612, 618
        }
    }
}
