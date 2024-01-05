namespace E_Commerce.Models
{
    public class ModelProp
    {
        public Proprietaire proprietaire { get; set; }
        public List<Transaction> ListeTransactions { get; set; }
        public List<Transaction> ListeTransactionsRecent { get; set; }
        public List<Produit> ListeProduits { get; set; }
        public List<Produit> ListeProduitsRecents { get; set; }
        public Produit Ajouter { get; set; }
        public List<Categorie> Categories { get; set; }
        public List<Scategorie> Scategories { get; set; }
        public int CountProduits { get; set; }
        public double CountSales { get; set; }
        public List<IFormFile> Images { get; set; }


    }
}
