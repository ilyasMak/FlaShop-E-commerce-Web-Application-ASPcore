namespace E_Commerce.Models
{
    public class AccueilClient
    {
        public List<Produit> produits { get; set; }
        public int count { get; set; }
        public List<Categorie> categories { get; set; }
        public bool LoggedIn { get; set; }

        public Client user { get; set; }

    }
}
