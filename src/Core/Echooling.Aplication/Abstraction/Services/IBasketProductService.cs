namespace Echooling.Aplication.Abstraction.Services;

public interface IBasketProductService
{
    Task RemoveAsync(Guid Id);
}
