namespace E_Commerce.Models;

public partial class Evaluation
{
    public int IdEvaluation { get; set; }

    public int IdClient { get; set; }

    public int IdProduit { get; set; }

    public int? Etoiles { get; set; }

    public virtual Client IdClientNavigation { get; set; } = null!;

    public virtual Produit IdProduitNavigation { get; set; } = null!;
}
