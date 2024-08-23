﻿// <auto-generated />
using System;
using Basketball;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Basketball.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240823080906_RestrictTeamNameLength")]
    partial class RestrictTeamNameLength
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Basketball.Entities.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AwayTeamID")
                        .HasColumnType("int");

                    b.Property<int>("AwayTeamScore")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("DatePlayed")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("HomeTeamID")
                        .HasColumnType("int");

                    b.Property<int>("HomeTeamScore")
                        .HasColumnType("int");

                    b.Property<int>("WinnerTeamID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Basketball.Entities.GameTeam", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("GameId", "TeamId");

                    b.HasIndex("TeamId");

                    b.ToTable("GameTeams");
                });

            modelBuilder.Entity("Basketball.Entities.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Basketball.Entities.Statistic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<double>("ThreePts")
                        .HasColumnType("float");

                    b.Property<double>("TotalPercent")
                        .HasColumnType("float");

                    b.Property<double>("TwoPts")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId")
                        .IsUnique();

                    b.ToTable("Statistics");
                });

            modelBuilder.Entity("Basketball.Entities.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Basketball.Entities.GameTeam", b =>
                {
                    b.HasOne("Basketball.Entities.Game", "Game")
                        .WithMany("GameTeams")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Basketball.Entities.Team", "Team")
                        .WithMany("GameTeams")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Basketball.Entities.Player", b =>
                {
                    b.HasOne("Basketball.Entities.Team", "Team")
                        .WithMany("Players")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Basketball.Entities.Statistic", b =>
                {
                    b.HasOne("Basketball.Entities.Player", "Player")
                        .WithOne("Statistics")
                        .HasForeignKey("Basketball.Entities.Statistic", "PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("Basketball.Entities.Game", b =>
                {
                    b.Navigation("GameTeams");
                });

            modelBuilder.Entity("Basketball.Entities.Player", b =>
                {
                    b.Navigation("Statistics")
                        .IsRequired();
                });

            modelBuilder.Entity("Basketball.Entities.Team", b =>
                {
                    b.Navigation("GameTeams");

                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
