using Echooling.Aplication.Abstraction.Repository.Basket;
using Echooling.Persistance.Contexts;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.Implementations.Repositories.BasketReposiyories;

public class BasketReadRepository : ReadRepository<Basket>, IBasketReadRepository
{
    public BasketReadRepository(AppDbContext context) : base(context)
    {
    }
}
