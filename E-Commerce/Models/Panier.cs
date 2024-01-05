namespace E_Commerce.Models;

public partial class Panier
{
    public int IdPanier { get; set; }

    public int IdClient { get; set; }

    public int IdProduit { get; set; }

    public int? QteProduit { get; set; }

    public virtual Client IdClientNavigation { get; set; } = null!;

    public virtual Produit IdProduitNavigation { get; set; } = null!;
}
