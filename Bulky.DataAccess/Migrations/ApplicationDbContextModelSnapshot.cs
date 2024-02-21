﻿// <auto-generated />
using Bulky.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bulky.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Bulky.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayOrder = 1,
                            Name = "Action"
                        },
                        new
                        {
                            Id = 2,
                            DisplayOrder = 2,
                            Name = "Scifi"
                        },
                        new
                        {
                            Id = 3,
                            DisplayOrder = 3,
                            Name = "History"
                        });
                });

            modelBuilder.Entity("Bulky.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ListPrice")
                        .HasColumnType("float");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<double>("Price100")
                        .HasColumnType("float");

                    b.Property<double>("Price50")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "Billy Spark",
                            CategoryId = 1,
                            Description = "Lorem",
                            ISBN = "SWD9999901",
                            ImageUrl = "",
                            ListPrice = 99.0,
                            Price = 90.0,
                            Price100 = 80.0,
                            Price50 = 85.0,
                            Title = "Fortune Of Time"
                        },
                        new
                        {
                            Id = 2,
                            Author = "Aryan Rand",
                            CategoryId = 1,
                            Description = "Lorem",
                            ISBN = "SWD9999902",
                            ImageUrl = "",
                            ListPrice = 100.0,
                            Price = 95.0,
                            Price100 = 95.0,
                            Price50 = 90.0,
                            Title = "Back To Eden"
                        },
                        new
                        {
                            Id = 3,
                            Author = "Jhumpa Lahiri",
                            CategoryId = 2,
                            Description = "the PEN/Hemingway Award, and the highest critical praise for its grace, acuity, and compassion in detailing lives transported from India to America.",
                            ISBN = "SWD9999903",
                            ImageUrl = "",
                            ListPrice = 115.0,
                            Price = 100.0,
                            Price100 = 90.0,
                            Price50 = 95.0,
                            Title = "The Namesake"
                        },
                        new
                        {
                            Id = 4,
                            Author = "Suzanne Collins",
                            CategoryId = 2,
                            Description = " Without really meaning to, she becomes a contender. But if she is to win, she will have to start making choices that weight survival against humanity and life against love",
                            ISBN = "SWD9999904",
                            ImageUrl = "",
                            ListPrice = 200.0,
                            Price = 190.0,
                            Price100 = 150.0,
                            Price50 = 180.0,
                            Title = "The Hunger Games"
                        },
                        new
                        {
                            Id = 5,
                            Author = "Jane Austen",
                            CategoryId = 3,
                            Description = " And Jane Austen's radiant wit sparkles as her characters dance a delicate quadrille of flirtation and intrigue, making this book the most superb comedy of manners of Regency England.",
                            ISBN = "9780679783268",
                            ImageUrl = "",
                            ListPrice = 400.0,
                            Price = 350.0,
                            Price100 = 300.0,
                            Price50 = 320.0,
                            Title = "Pride and Prejudice"
                        },
                        new
                        {
                            Id = 6,
                            Author = "Elle Kennedy",
                            CategoryId = 2,
                            Description = "She's about to make a deal with the college bad boy... Hannah Wells has finally found someone who turns her on. But while she might be confident in every other area of her life, she's carting around a full set of baggage when it comes to sex and seduction.",
                            ISBN = "9781775293934",
                            ImageUrl = "",
                            ListPrice = 150.0,
                            Price = 140.0,
                            Price100 = 100.0,
                            Price50 = 120.0,
                            Title = "The Deal"
                        },
                        new
                        {
                            Id = 7,
                            Author = "Gillian Flynn",
                            CategoryId = 3,
                            Description = "These are the questions Nick Dunne finds himself asking on the morning of his fifth wedding anniversary when his wife Amy suddenly disappears. The police suspect Nick.",
                            ISBN = "9781775293935",
                            ImageUrl = "",
                            ListPrice = 460.0,
                            Price = 420.0,
                            Price100 = 380.0,
                            Price50 = 400.0,
                            Title = "Gone Girl"
                        });
                });

            modelBuilder.Entity("Bulky.Models.Product", b =>
                {
                    b.HasOne("Bulky.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
