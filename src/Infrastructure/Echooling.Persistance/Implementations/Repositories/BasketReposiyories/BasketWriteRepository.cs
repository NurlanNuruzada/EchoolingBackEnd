using Echooling.Aplication.Abstraction.Repository.Basket;
using Echooling.Persistance.Contexts;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.Implementations.Repositories.BasketReposiyories;

public class BasketWriteRepository : WriteRepository<Basket>, IBasketWriteRepository
{
    public BasketWriteRepository(AppDbContext context) : base(context)
    {
    }
}
