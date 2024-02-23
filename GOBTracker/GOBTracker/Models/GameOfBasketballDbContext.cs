using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GOBTracker.Models;

public partial class GameOfBasketballDbContext : DbContext
{
    public GameOfBasketballDbContext()
    {
    }

    public GameOfBasketballDbContext(DbContextOptions<GameOfBasketballDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<PlayerTeam> PlayerTeams { get; set; }

    public virtual DbSet<Stat> Stats { get; set; }

    public virtual DbSet<StatType> StatTypes { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>(entity =>
        {
            entity.Property(e => e.GameId).HasColumnName("gameID");
            entity.Property(e => e.GameDateTime)
                .HasColumnType("datetime")
                .HasColumnName("gameDateTime");
            entity.Property(e => e.MyTeamId).HasColumnName("myTeamID");
            entity.Property(e => e.OpponentTeamId).HasColumnName("opponentTeamID");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.Property(e => e.PlayerId).HasColumnName("playerID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("lastName");
        });

        modelBuilder.Entity<PlayerTeam>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.PlayerId).HasColumnName("PlayerID");
            entity.Property(e => e.TeamId).HasColumnName("TeamID");
        });

        modelBuilder.Entity<Stat>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.GameId).HasColumnName("GameID");
            entity.Property(e => e.PlayerTeamId).HasColumnName("PlayerTeamID");
            entity.Property(e => e.StatTypeId).HasColumnName("StatTypeID");
        });

        modelBuilder.Entity<StatType>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.StatName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("PK_Team");

            entity.Property(e => e.TeamId).HasColumnName("teamID");
            entity.Property(e => e.TeamName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("teamName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
