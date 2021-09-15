using ProvaCandidato.Data.Entidade;
using System.Data.Entity;

namespace ProvaCandidato.Data
{
    public class ContextoPrincipal : DbContext
  {
    const string CONNECTION_STRING = @"Server=(localdb)\MSSQLLocalDB;Database=provacandidato;Trusted_Connection=True;";
    public ContextoPrincipal() : base(CONNECTION_STRING) { }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Cidade> Cidades { get; set; }
    }
}
