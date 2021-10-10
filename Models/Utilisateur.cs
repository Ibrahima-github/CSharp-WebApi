namespace WebAPI.Models
{
    public class Utilisateur
    {
        public int UtilisateurId { get; set; }
        public string UtilisateurUsername { get; set; }
        public string UtilisateurEmailAddress { get; set; }
        public string UtilisateurPassword { get; set; }
        public bool IsAdmin { get; set; }
    }
}
