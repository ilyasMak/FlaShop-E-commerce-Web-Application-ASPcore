namespace E_Commerce.Models
{
    public class SharedClientProp
    {
        public int Id { get; set; }
        public string type { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string Adress_Activ { get; set; }

        public List<Transaction> transactions { get; set; }

        public List<Produit> produits { get; set; }
    }
}
