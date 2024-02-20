using Ecooling.Domain.Entites;

namespace Echooling.Aplication.Abstraction.Services;

public interface IBasketService
{
    Task AddBasketAsync(Guid id, string AppuserId);
    Task<List<BasketProduct>> GetBasketProduct(string AppuserId);
    Task DeleteBasktetAsyc(Guid id, string AppuserId);
    Task<int> getBasketCount(string AppuserId);
    Task DeleteBasketItem(Guid CartId, string AppuserId);

}
