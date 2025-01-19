namespace DOTNET_Control.Models
{
    public class Favoris
    {
        public int Id { get; set; }
        public string UserId { get; set; } // ID de l'utilisateur (lié à l'authentification)
        public int BookId { get; set; } // ID du livre
        public DateTime AddedDate { get; set; }

        // Relations
        public Book Book { get; set; }
        public ApplicationUser User { get; set; } // ApplicationUser pour Identity
    }
}
