namespace DOTNET_Control.Models
{
    public class UserBook
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }

}
