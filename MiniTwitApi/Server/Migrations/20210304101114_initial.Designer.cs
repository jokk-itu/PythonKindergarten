﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MiniTwitApi.Server.Entities;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MiniTwitApi.Server.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20210304101114_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("MiniTwitApi.Server.Entities.Follower", b =>
                {
                    b.Property<int>("WhoId")
                        .HasColumnType("integer");

                    b.Property<int>("WhomId")
                        .HasColumnType("integer");

                    b.HasKey("WhoId", "WhomId");

                    b.HasIndex("WhomId");

                    b.ToTable("Followers");
                });

            modelBuilder.Entity("MiniTwitApi.Server.Entities.Message", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<string>("AuthorUsername")
                        .HasColumnType("text");

                    b.Property<int>("Flagged")
                        .HasColumnType("integer");

                    b.Property<int>("PubDate")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("UserId1")
                        .HasColumnType("integer");

                    b.HasKey("MessageId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("UserId1");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("MiniTwitApi.Server.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MiniTwitApi.Server.Entities.Follower", b =>
                {
                    b.HasOne("MiniTwitApi.Server.Entities.User", "Who")
                        .WithMany("Followers")
                        .HasForeignKey("WhoId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("MiniTwitApi.Server.Entities.User", "Whom")
                        .WithMany()
                        .HasForeignKey("WhomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Who");

                    b.Navigation("Whom");
                });

            modelBuilder.Entity("MiniTwitApi.Server.Entities.Message", b =>
                {
                    b.HasOne("MiniTwitApi.Server.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiniTwitApi.Server.Entities.User", null)
                        .WithMany("Messages")
                        .HasForeignKey("UserId1");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MiniTwitApi.Server.Entities.User", b =>
                {
                    b.Navigation("Followers");

                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
