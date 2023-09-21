namespace LibraryAPI.DTOs
{
    public class CreateBookDTO
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string PublishingYear { get; set; }
        public int NumberInStock { get; set; }
    }
}
