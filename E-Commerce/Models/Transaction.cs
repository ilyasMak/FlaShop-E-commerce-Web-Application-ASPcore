namespace E_Commerce.Models;

public partial class Transaction
{
    public int NumTransaction { get; set; }

    public int IdClient { get; set; }

    public int IdProduit { get; set; }

    public double PrixTransaction { get; set; }

    public DateOnly DateTransaction { get; set; }

    public int? QteProduit { get; set; }

    public string? Delivre { get; set; }

    public virtual Client IdClientNavigation { get; set; } = null!;

    public virtual Produit IdProduitNavigation { get; set; } = null!;
}
