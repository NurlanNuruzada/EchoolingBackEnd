using Echooling.Aplication.Abstraction.Repository.BasketProduct;
using Echooling.Persistance.Contexts;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.Implementations.Repositories.BasketProductRepositories;

public class BasketProductReadRepository : ReadRepository<BasketProduct>, IBasketProductReadRepository
{
    public BasketProductReadRepository(AppDbContext context) : base(context)
    {
    }
}
