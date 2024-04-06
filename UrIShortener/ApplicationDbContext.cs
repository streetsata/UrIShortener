using Microsoft.EntityFrameworkCore;
using UrIShortener.Entities;
using UrIShortener.Services;

namespace UrIShortener
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<ShortenedUrl> ShortenedUrls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortenedUrl>(builder =>
            {
                builder.Property(s => s.Code).HasMaxLength(UrlShorteningService.NumberOfCharsInShortUrl);

                builder.HasIndex(s => s.Code).IsUnique();
            });
        }
    }
}
