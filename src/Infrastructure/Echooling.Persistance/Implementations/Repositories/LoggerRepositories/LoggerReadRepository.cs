using Echooling.Aplication.Abstraction.Repository.LoggerRepositories;
using Echooling.Persistance.Contexts;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.Implementations.Repositories.LoggerRepositories;

public class LoggerReadRepository : ReadRepository<Logger>, ILoggerReadRepository
{
    public LoggerReadRepository(AppDbContext context) : base(context) { }
}
