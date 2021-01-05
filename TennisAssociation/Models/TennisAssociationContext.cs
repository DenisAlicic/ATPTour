using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TennisAssociation.Models
{
    public partial class TennisAssociationContext : DbContext
    {
        public TennisAssociationContext()
        {
        }

        public TennisAssociationContext(DbContextOptions<TennisAssociationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<Player> Players { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=EN510626\\SQLExpress;Database=TennisAssociation;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Match>(entity =>
            {
                entity.ToTable("matches");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.FirstPlayer)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("firstPlayer");

                entity.Property(e => e.HeadToHeadFirst).HasColumnName("headToHeadFirst");

                entity.Property(e => e.HeadToHeadSecond).HasColumnName("headToHeadSecond");

                entity.Property(e => e.ResultFirst).HasColumnName("resultFirst");

                entity.Property(e => e.ResultSecond).HasColumnName("resultSecond");

                entity.Property(e => e.SecondPlayer)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("secondPlayer");

                entity.Property(e => e.Tournament)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tournament");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("players");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.BestRankingDouble).HasColumnName("bestRankingDouble");

                entity.Property(e => e.BestRankingSingle).HasColumnName("bestRankingSingle");

                entity.Property(e => e.Birth)
                    .HasColumnType("date")
                    .HasColumnName("birth");

                entity.Property(e => e.Country)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("country");

                entity.Property(e => e.CurrentRankingDouble).HasColumnName("currentRankingDouble");

                entity.Property(e => e.CurrentRankingSingle).HasColumnName("currentRankingSingle");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("firstName");

                entity.Property(e => e.Hand)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("hand");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.Img).HasColumnName("img");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lastName");

                entity.Property(e => e.Sex)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("sex");

                entity.Property(e => e.Weight).HasColumnName("weight");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
