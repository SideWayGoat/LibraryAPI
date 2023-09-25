namespace LibraryAPI.DTOs
{
    public class UpdateBookDTO
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public int NumberInStock { get; set; }
        public string PublishingYear { get; set; }
    }
}
