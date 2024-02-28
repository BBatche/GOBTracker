using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GOBTracker.Models;

public partial class GobtrackerDbContext : DbContext
{
    public GobtrackerDbContext()
    {
    }

    public GobtrackerDbContext(DbContextOptions<GobtrackerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AllRawStat> AllRawStats { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<OpponentTeamGameStat> OpponentTeamGameStats { get; set; }

    public virtual DbSet<OurTeamGameStat> OurTeamGameStats { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<PlayerGameStat> PlayerGameStats { get; set; }

    public virtual DbSet<PlayerTeam> PlayerTeams { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Stat> Stats { get; set; }

    public virtual DbSet<StatType> StatTypes { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamRoster> TeamRosters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AllRawStat>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("AllRawStats");

            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StatName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StatNameAbr).HasMaxLength(5);
            entity.Property(e => e.StatValue).HasColumnType("decimal(8, 4)");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.OpponentTeam).WithMany(p => p.GameOpponentTeams)
                .HasForeignKey(d => d.OpponentTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OpponenetTeamID_TeamID");

            entity.HasOne(d => d.OurTeam).WithMany(p => p.GameOurTeams)
                .HasForeignKey(d => d.OurTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OurTeamID_TeamID");
        });

        modelBuilder.Entity<OpponentTeamGameStat>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("OpponentTeamGameStats");

            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TeamName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TotalAssists).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.TotalBlocks).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.TotalDefRebounds).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.TotalFouls).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.TotalOffRebounds).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.TotalPoints).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.TotalSteals).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.TotalThreePmade)
                .HasColumnType("decimal(38, 4)")
                .HasColumnName("TotalThreePMade");
            entity.Property(e => e.TotalThreePmiss)
                .HasColumnType("decimal(38, 4)")
                .HasColumnName("TotalThreePMiss");
            entity.Property(e => e.TotalTurnovers).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.TotalTwoPmade)
                .HasColumnType("decimal(38, 4)")
                .HasColumnName("TotalTwoPMade");
            entity.Property(e => e.TotalTwoPmiss)
                .HasColumnType("decimal(38, 4)")
                .HasColumnName("TotalTwoPMiss");
        });

        modelBuilder.Entity<OurTeamGameStat>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("OurTeamGameStats");

            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TeamName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TotalAssists).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.TotalBlocks).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.TotalDefRebounds).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.TotalFouls).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.TotalOffRebounds).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.TotalPoints).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.TotalSteals).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.TotalThreePmade)
                .HasColumnType("decimal(38, 4)")
                .HasColumnName("TotalThreePMade");
            entity.Property(e => e.TotalThreePmiss)
                .HasColumnType("decimal(38, 4)")
                .HasColumnName("TotalThreePMiss");
            entity.Property(e => e.TotalTurnovers).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.TotalTwoPmade)
                .HasColumnType("decimal(38, 4)")
                .HasColumnName("TotalTwoPMade");
            entity.Property(e => e.TotalTwoPmiss)
                .HasColumnType("decimal(38, 4)")
                .HasColumnName("TotalTwoPMiss");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PlayerGameStat>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("PlayerGameStats");

            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PlayerId).HasColumnName("PlayerID");
            entity.Property(e => e.Total2ptsMade)
                .HasColumnType("decimal(38, 4)")
                .HasColumnName("Total 2pts Made");
            entity.Property(e => e.Total3ptsMade)
                .HasColumnType("decimal(38, 4)")
                .HasColumnName("Total 3pts Made");
            entity.Property(e => e.TotalPoints)
                .HasColumnType("decimal(38, 4)")
                .HasColumnName("Total_Points");
        });

        modelBuilder.Entity<PlayerTeam>(entity =>
        {
            entity.HasIndex(e => new { e.PlayerId, e.TeamId }, "IX_PlayerTeamUnique").IsUnique();

            entity.HasOne(d => d.Player).WithMany(p => p.PlayerTeams)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlayerTeams_Players");

            entity.HasOne(d => d.Team).WithMany(p => p.PlayerTeams)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlayerTeams_Teams");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Schedule");

            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Opponent)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OurTeam)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Stat>(entity =>
        {
            entity.Property(e => e.StatValue).HasColumnType("decimal(8, 4)");

            entity.HasOne(d => d.Game).WithMany(p => p.Stats)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stats_Games");

            entity.HasOne(d => d.PlayerTeam).WithMany(p => p.Stats)
                .HasForeignKey(d => d.PlayerTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stats_PlayerTeams");

            entity.HasOne(d => d.StatType).WithMany(p => p.Stats)
                .HasForeignKey(d => d.StatTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stats_StatTypes");
        });

        modelBuilder.Entity<StatType>(entity =>
        {
            entity.Property(e => e.StatName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StatNameAbr).HasMaxLength(5);
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.Property(e => e.TeamName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TeamRoster>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TeamRosters");

            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TeamName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
