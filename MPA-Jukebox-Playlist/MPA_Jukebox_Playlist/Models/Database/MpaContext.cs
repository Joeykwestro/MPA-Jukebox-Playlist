using Microsoft.EntityFrameworkCore;

namespace MPA_Jukebox_Playlist.MPA_Jukebox_Playlist.Models.Database
{
    public partial class MpaContext : DbContext
    {
        public MpaContext(DbContextOptions<MpaContext> options)
    : base(options)
        {
        }

        public virtual DbSet<Genres> Genres { get; set; } = null!;
        public virtual DbSet<Playlists> Playlists { get; set; } = null!;
        public virtual DbSet<Saved_Songs> Saved_Songs { get; set; } = null!;
        public virtual DbSet<Songs> Songs { get; set; } = null!;
        public virtual DbSet<Users> Users { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genres>(entity =>
            {
                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.Type).HasColumnType("Type");
            });

            modelBuilder.Entity<Playlists>(entity =>
            {
                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.UserID).HasColumnName("UserID");

                entity.Property(e => e.Title).HasColumnType("Title");

                entity.Property(e => e.SongsID).HasColumnType("SongsID");
            });

            modelBuilder.Entity<Saved_Songs>(entity =>
            {
                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.PlaylistID).HasColumnName("PlaylistID");

                entity.Property(e => e.SongID).HasColumnName("SongID");
            });

            modelBuilder.Entity<Songs>(entity =>
            {
                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.Title).HasColumnName("Title");

                entity.Property(e => e.Artist).HasColumnName("Artist");

                entity.Property(e => e.GenreID).HasColumnName("GenreID");

                entity.Property(e => e.Duration).HasColumnName("Duration");

            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.Username).HasColumnName("Username");

                entity.Property(e => e.Password).HasColumnName("Duration");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
