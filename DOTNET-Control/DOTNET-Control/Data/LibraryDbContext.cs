using DOTNET_Control.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

public class LibraryDbContext : IdentityDbContext<ApplicationUser>
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<UserBook> UserBooks { get; set; }
    public DbSet<Favoris> Favorites { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Favoris>()
             .HasOne(f => f.Book)
             .WithMany()
             .HasForeignKey(f => f.BookId);
        builder.Entity<Favoris>()
            .HasOne(f => f.User)
            .WithMany()
            .HasForeignKey(f => f.UserId);




        builder.Entity<UserBook>()
            .HasKey(ub => new { ub.UserId, ub.BookId });

        builder.Entity<UserBook>()
            .HasOne(ub => ub.User)
            .WithMany(u => u.UserBooks)
            .HasForeignKey(ub => ub.UserId);

        builder.Entity<UserBook>()
            .HasOne(ub => ub.Book)
            .WithMany(b => b.UserBooks)
            .HasForeignKey(ub => ub.BookId);

        // Seed Authors
        builder.Entity<Author>().HasData(
            new Author { Id = 1, Name = "J.K. Rowling" },
            new Author { Id = 2, Name = "George R.R. Martin" },
            new Author { Id = 3, Name = "J.R.R. Tolkien" }
        );

        // Seed Categories
        builder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Fantasy" },
            new Category { Id = 2, Name = "Science Fiction" },
            new Category { Id = 3, Name = "Mystery" }
        );

        // Seed Publishers
        builder.Entity<Publisher>().HasData(
            new Publisher { Id = 1, Name = "Bloomsbury" },
            new Publisher { Id = 2, Name = "Bantam Books" },
            new Publisher { Id = 3, Name = "HarperCollins" }
        );

        // Seed Books
        builder.Entity<Book>().HasData(
            new Book
            {
                Id = 1,
                Title = "Harry Potter and the Sorcerer's Stone",
                AuthorId = 1,
                PublisherId = 1,
                CategoryId = 1
            },
            new Book
            {
                Id = 2,
                Title = "A Game of Thrones",
                AuthorId = 2,
                PublisherId = 2,
                CategoryId = 1
            },
            new Book
            {
                Id = 3,
                Title = "The Hobbit",
                AuthorId = 3,
                PublisherId = 3,
                CategoryId = 1
            }
        );


        // Seed a default admin user (optional, using IdentityUser as a base class)
        var adminId = "00000000-ffff-aaaa-bbbb-cccccccccccc"; // Static value instead of Guid.NewGuid()
        var hasher = new PasswordHasher<ApplicationUser>();
        builder.Entity<ApplicationUser>().HasData(
            new ApplicationUser
            {
                Id = adminId,
                UserName = "Kabil Youssef",
                NormalizedUserName = "KABIL YOUSSEF",
                Email = "admin@library.com",
                NormalizedEmail = "ADMIN@LIBRARY.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Admin123"),
                SecurityStamp = "f1e1d2c3-b4a5-6789-abcd-ef0123456789"
                ,
                isAdmin = true,

            }
        );
    }


}
