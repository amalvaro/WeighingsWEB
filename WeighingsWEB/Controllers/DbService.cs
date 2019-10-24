using System.Data.Entity;
using System.Threading.Tasks;

namespace WeighingsWEB.Controllers
{
    public class DbService : IDbService
    {
        public WeighingDBConfiguration Configuration => throw new System.NotImplementedException();

        public DbContext CreateContext()
        {
            throw new System.NotImplementedException();
        }

        public Task<DbContext> CreateContextAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}