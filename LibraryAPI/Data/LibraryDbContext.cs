using LibraryBookModels;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>().HasData(new Book()
            {
                Id = 1,
                Author = "Patrick Rothfuss",
                Title = "The Name Of The Wind",
                Description = "tale about Kvothe, a legendary and extraordinary figure whose many heroic " +
                "deeds are the subject of local lore.",
                Genre = "Fantasy",
                NumberInStock = 6,
                PublishingYear = "2007"
            },
            new Book()
            {
                Id = 2,
                Author = "Frank Herbert",
                Title = "Dune",
                Description = "Set on the desert planet Arrakis, Dune is the story of the boy Paul Atreides, " +
                "heir to a noble family tasked with ruling an inhospitable world where the only thing of value " +
                "is the “spice” melange, a drug capable of extending life and enhancing consciousness.",
                Genre = "Sci-Fi",
                NumberInStock = 4,
                PublishingYear = "1965"
            },
            new Book()
            {
                Id = 3,
                Author = "Dan Simmons",
                Title = "Hyperion Cantos",
                Description = "On the eve of Armageddon, with the entire galaxy at war, " +
                "seven pilgrims set forth on a final voyage to" +
                " Hyperion seeking the answers to the unsolved riddles of their lives.",
                Genre = "Sci-Fi",
                NumberInStock = 1,
                PublishingYear = "1989"
            });
        }
    }
}
