using System.Data.Entity;
using WpfApp1.Requests;

public class Context : DbContext
{
    public Context() : base("DefaultConnection")
    {

    }
    public DbSet<Client> clientsTable { get; set; }
}
