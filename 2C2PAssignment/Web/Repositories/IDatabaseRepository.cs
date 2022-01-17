using System.Threading.Tasks;

namespace Web.Repositories
{
    public interface IDatabaseRepository
    {
        Task Create(string dbName);
    }
}
