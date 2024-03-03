using Bulky.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; } //create table
        public DbSet<Product> Products { get; set; }
        //DbSet for extend Identity User
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },

                new Category { Id = 2, Name = "Scifi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
               );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "Fortune Of Time",
                    Author = "Billy Spark",
                    Description = "Lorem",
                    ISBN = "SWD9999901",
                    ListPrice = 99,
                    Price = 90,
                    Price50 = 85,
                    Price100 = 80,
                    CategoryId = 1,
                    ImageUrl=""
                },
                new Product
                {
                    Id = 2,
                    Title = "Back To Eden",
                    Author = "Aryan Rand",
                    Description = "Lorem",
                    ISBN = "SWD9999902",
                    ListPrice = 100,
                    Price = 95,
                    Price50 = 90,
                    Price100 = 95,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 3,
                    Title = "The Namesake",
                    Author = "Jhumpa Lahiri",
                    Description = "the PEN/Hemingway Award, and the highest critical praise for its grace, acuity, and compassion in detailing lives transported from India to America.",
                    ISBN = "SWD9999903",
                    ListPrice = 115,
                    Price = 100,
                    Price50 = 95,
                    Price100 = 90,
                    CategoryId = 2,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 4,
                    Title = "The Hunger Games",
                    Author = "Suzanne Collins",
                    Description = " Without really meaning to, she becomes a contender. But if she is to win, she will have to start making choices that weight survival against humanity and life against love",
                    ISBN = "SWD9999904",
                    ListPrice = 200,
                    Price = 190,
                    Price50 = 180,
                    Price100 = 150,
                    CategoryId = 2,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 5,
                    Title = "Pride and Prejudice",
                    Author = "Jane Austen",
                    Description = " And Jane Austen's radiant wit sparkles as her characters dance a delicate quadrille of flirtation and intrigue, making this book the most superb comedy of manners of Regency England.",
                    ISBN = "9780679783268",
                    ListPrice = 400,
                    Price = 350,
                    Price50 = 320,
                    Price100 = 300,
                    CategoryId = 3,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 6,
                    Title = "The Deal",
                    Author = "Elle Kennedy",
                    Description = "She's about to make a deal with the college bad boy... Hannah Wells has finally found someone who turns her on. But while she might be confident in every other area of her life, she's carting around a full set of baggage when it comes to sex and seduction.",
                    ISBN = "9781775293934",
                    ListPrice = 150,
                    Price = 140,
                    Price50 = 120,
                    Price100 = 100,
                    CategoryId = 2,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 7,
                    Title = "Gone Girl",
                    Author = "Gillian Flynn",
                    Description = "These are the questions Nick Dunne finds himself asking on the morning of his fifth wedding anniversary when his wife Amy suddenly disappears. The police suspect Nick.",
                    ISBN = "9781775293935",
                    ListPrice = 460,
                    Price = 420,
                    Price50 = 400,
                    Price100 = 380,
                    CategoryId = 3,
                    ImageUrl = ""
                }
                );
        }
    }
}
