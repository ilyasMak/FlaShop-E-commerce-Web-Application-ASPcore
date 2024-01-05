namespace E_Commerce.Models
{
    public class AdminDashBoard
    {

        public int UserCount { get; set; }
        public int OwnerCount { get; set; }
        public double TotalTransactionPrice { get; set; }

        public List<Proprietaire> UsersBlocked
        {
            get; set;
        }
        public List<Transaction> RecentTransactions { get; set; }



    }
}
