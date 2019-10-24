using System.Data.Entity;
using System.Threading.Tasks;

namespace WeighingsWEB.Controllers
{
    public interface IDbService
    {
        WeighingDBConfiguration Configuration { get; }

        DbContext CreateContext();

        Task<DbContext> CreateContextAsync();
    }
}