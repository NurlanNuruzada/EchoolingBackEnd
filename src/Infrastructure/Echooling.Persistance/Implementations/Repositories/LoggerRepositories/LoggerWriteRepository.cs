using Echooling.Aplication.Abstraction.Repository.LoggerRepositories;
using Echooling.Persistance.Contexts;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.Implementations.Repositories.LoggerRepositories;
internal class LoggerWriteRepository : WriteRepository<Logger>, ILoggerWriteRepository
{
    public LoggerWriteRepository(AppDbContext context) : base(context) { }
}