using System;
using Blog.Domain.FunctionalCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Blog.Infrastructure
{
    public class BlogContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>(p =>
                {
                    p.HasKey(post => post.PostId);
                    p.Property(post => post.PostId)
                        .UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
                    p.Property<string>("_title");
                });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server = (localdb)\\mssqllocaldb; Database = BlogWriteData; Trusted_Connection = True; ")
                .UseLoggerFactory(BuildConsoleLoggerFactory)
                .EnableSensitiveDataLogging();
        }

        private static readonly LoggerFactory BuildConsoleLoggerFactory
            = new LoggerFactory(new[] {
                new ConsoleLoggerProvider((category, level)
                    => category == DbLoggerCategory.Database.Command.Name
                       && level == LogLevel.Information, true) });
    }

    
}
