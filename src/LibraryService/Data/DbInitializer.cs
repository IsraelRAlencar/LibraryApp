using LibraryService.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Data;

public class DbInitializer
{
    public static void InitDb(WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        SeedData(scope.ServiceProvider.GetService<LibraryDbContext>());
    }

    private static void SeedData(LibraryDbContext context)
    {
        context.Database.Migrate();

        if (context.Books.Any())
        {
            Console.WriteLine("The database has already been seeded.");
            return;
        }

        Console.WriteLine("Seeding the database...");

        List<Category> categories = new()
        {
            new Category { 
                Id = Guid.NewGuid(),
                Name = "Fiction",
                Description = "Books that explore imaginary worlds and characters",
                CreatedAt = DateTime.UtcNow,
            },
            new Category { 
                Id = Guid.NewGuid(),
                Name = "Romance",
                Description = "Books that focus on romantic relationships",
                CreatedAt = DateTime.UtcNow,
            },
            new Category { 
                Id = Guid.NewGuid(),
                Name = "Drama",
                Description = "Books that focus on emotional themes",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Categories.AddRange(categories);

        List<Book> books = new()
        {
            new Book { 
                Id = Guid.NewGuid(),
                Title = "The Great Gatsby",
                Author = "F. Scott Fitzgerald",
                Categories = [categories[0]],
                Year = 1925,
                Images = new List<Image> { new Image { 
                        Id = Guid.NewGuid(),
                        ImageUrl = "https://i0.wp.com/americanwritersmuseum.org/wp-content/uploads/2018/02/CK-3.jpg?resize=267%2C400&ssl=1",
                        CreatedAt = DateTime.UtcNow,
                    }
                },
                Publisher = "Charles Scribner's Sons",
                CreatedAt = DateTime.UtcNow,
            },
            new Book { 
                Id = Guid.NewGuid(),
                Title = "Pride and Prejudice",
                Author = "Jane Austen",
                Categories = [categories[1]],
                Year = 1813,
                Images = new List<Image> { new Image { 
                        Id = Guid.NewGuid(),
                        ImageUrl = "https://m.media-amazon.com/images/I/81csyfAHE8L._AC_UF1000,1000_QL80_.jpg",
                        CreatedAt = DateTime.UtcNow,
                    }
                },
                Publisher = "T. Egerton, Whitehall",
                CreatedAt = DateTime.UtcNow,
            },
            new Book { 
                Id = Guid.NewGuid(),
                Title = "To Kill a Mockingbird",
                Author = "Harper Lee",
                Categories = [categories[2]],
                Year = 1960,
                Images = new List<Image> { new Image { 
                        Id = Guid.NewGuid(),
                        ImageUrl = "https://m.media-amazon.com/images/I/81aY1lxk+9L._AC_UF894,1000_QL80_.jpg",
                        CreatedAt = DateTime.UtcNow,
                    }
                },   
                Publisher = "J.B. Lippincott & Co.",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Books.AddRange(books);

        List<User> users = new()
        {
            new User { 
                Id = Guid.NewGuid(),
                Name = "Fulano",
                BirthDate = new DateTime(2002, 9, 23).ToUniversalTime(),
                Email = "fulano@email.com",
                Password = "fulano123",
                CreatedAt = DateTime.UtcNow,
            },
            new User { 
                Id = Guid.NewGuid(),
                Name = "Ciclano",
                BirthDate = new DateTime(2004, 10, 31).ToUniversalTime(),
                Email = "ciclano@email.com",
                Password = "ciclano123",
                CreatedAt = DateTime.UtcNow,
            }
        };

        context.Users.AddRange(users);

        List<Loan> loans = new List<Loan>
        {
            new Loan
            {
                Id = Guid.NewGuid(),
                Book = books[0],
                User = users[0],
                Observation = "Returned in good condition.",
                LoanDate = DateTime.UtcNow.AddDays(-15),
                ReturnDate = DateTime.UtcNow.AddDays(-5),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Loan
            {
                Id = Guid.NewGuid(),
                Book = books[1],
                User = users[1],
                Observation = "Overdue by 2 days.",
                LoanDate = DateTime.UtcNow.AddDays(-30),
                ReturnDate = DateTime.UtcNow.AddDays(-10),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };

        context.Loans.AddRange(loans);

        List<Reservation> reservations = new List<Reservation>
        {
            new Reservation
            {
                Id = Guid.NewGuid(),
                Book = books[2],
                User = users[0],
                Observation = "Waiting for pickup at the library.",
                ReservationDate = DateTime.UtcNow.AddDays(-2),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Reservation
            {
                Id = Guid.NewGuid(),
                Book = books[0],
                User = users[1],
                Observation = "Reserved for delivery to home address.",
                ReservationDate = DateTime.UtcNow.AddDays(-7),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };

        context.Reservations.AddRange(reservations);

        context.SaveChanges();
    }
}
