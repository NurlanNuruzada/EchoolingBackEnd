using Echooling.Aplication.Abstraction.Repository.BasketProduct;
using Echooling.Persistance.Contexts;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.Implementations.Repositories.BasketProductRepositories;

public class BasketProductWriteRepository : WriteRepository<BasketProduct>, IBasketProductWriteRepository
{
    public BasketProductWriteRepository(AppDbContext context) : base(context)
    {
    }
}
