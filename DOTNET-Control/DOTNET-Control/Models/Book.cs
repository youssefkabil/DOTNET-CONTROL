namespace DOTNET_Control.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<UserBook> UserBooks { get; set; }
        public string ImageUrl { get; set; } = "images/book1.jpg"; // Default value updated

    }

}
