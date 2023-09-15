using System.ComponentModel.DataAnnotations;

namespace LibraryBookModels
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string PublishingYear { get; set; }
        public int NumberInStock { get; set; }
    }
}