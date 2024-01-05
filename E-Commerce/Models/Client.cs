namespace E_Commerce.Models;

public partial class Client
{
    public int IdClient { get; set; }

    public string? NomClient { get; set; }

    public string? PrenomClient { get; set; }

    public string? EmailClient { get; set; }

    public string? MotPassClient { get; set; }

    public string? StatusClient { get; set; }

    public int IdAdresse { get; set; }

    public virtual ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();

    public virtual Adresse IdAdresseNavigation { get; set; } = null!;

    public virtual ICollection<Panier> Paniers { get; set; } = new List<Panier>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
