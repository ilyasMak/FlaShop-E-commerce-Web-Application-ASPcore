namespace E_Commerce.Models;

public partial class Image
{
    public int IdImage { get; set; }

    public string? Image1 { get; set; }

    public int IdProduit { get; set; }

    public virtual Produit IdProduitNavigation { get; set; } = null!;
}
