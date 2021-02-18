﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyApp.Entities;

namespace MiniTwitApi.Server.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("MyApp.Entities.Follower", b =>
                {
                    b.Property<int>("WhoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WhomId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("WhoUserUserId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("WhomUserUserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("WhoId", "WhomId");

                    b.HasIndex("WhoUserUserId");

                    b.HasIndex("WhomUserUserId");

                    b.ToTable("Gender");
                });

            modelBuilder.Entity("MyApp.Entities.Message", b =>
                {
                    b.Property<int>("messageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("authorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("authorUsername")
                        .HasColumnType("TEXT");

                    b.Property<int>("flagged")
                        .HasColumnType("INTEGER");

                    b.Property<int>("pubDate")
                        .HasColumnType("INTEGER");

                    b.Property<string>("text")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("messageId");

                    b.HasIndex("authorId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("MyApp.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PwHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MyApp.Entities.Follower", b =>
                {
                    b.HasOne("MyApp.Entities.User", "WhoUser")
                        .WithMany()
                        .HasForeignKey("WhoUserUserId");

                    b.HasOne("MyApp.Entities.User", "WhomUser")
                        .WithMany()
                        .HasForeignKey("WhomUserUserId");

                    b.Navigation("WhomUser");

                    b.Navigation("WhoUser");
                });

            modelBuilder.Entity("MyApp.Entities.Message", b =>
                {
                    b.HasOne("MyApp.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("authorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
