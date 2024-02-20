using AutoMapper;
using Echooling.Aplication.Abstraction.Repository.Basket;
using Echooling.Aplication.Abstraction.Services;
using Ecooling.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg;

namespace Echooling.Persistance.Implementations.Services;

public class BasketService : IBasketService
{
    private readonly IBasketReadRepository _basketReadRepository;
    private readonly IBasketWriteRepository _basketWriteRepository;
    private readonly IMapper _mapper;
    private readonly IBasketProductService _productService;
    private readonly ICourseService _courseService;
    private readonly IEventService _eventService;

    public BasketService(IBasketReadRepository basketReadRepository,
                         IBasketWriteRepository basketWriteRepository,
                         IMapper mapper,
                         IBasketProductService productService,
                         ICourseService courseService,
                         IEventService eventService)
    {
        _basketReadRepository = basketReadRepository;
        _basketWriteRepository = basketWriteRepository;
        _mapper = mapper;
        _productService = productService;
        _courseService = courseService;
        _eventService = eventService;
    }
    public async Task AddBasketAsync(Guid id, string AppuserId)
    {
        var basket = await _basketReadRepository.Table
                    .Include(x => x.BasketProducts)
                    .FirstOrDefaultAsync(x => x.AppUserId == AppuserId);

        var byCourse = await _courseService.getById(id);

        if (basket is null)
        {
            basket = new Basket()
            {
                AppUserId = AppuserId
            };
            await _basketWriteRepository.addAsync(basket);

            // Manually set the date properties for the newly created Basket.
            var now = DateTime.Now;
            basket.DateCreated = now;
            basket.DateModified = now;

            await _basketWriteRepository.SaveChangesAsync();
        }

        var basketProduct = basket.BasketProducts
             .FirstOrDefault(x => x.CourseId == id || x.EventsId == id && x.BasketId == basket.GuId);

        if (basketProduct is not null) basketProduct.Quantity = 1;
        else
        {
            if (byCourse is not null)
            {
                basketProduct = new BasketProduct()
                {
                    BasketId = basket.GuId,
                    CourseId = id,
                    EventsId = null,
                    Quantity = 1
                };
            }
            else
            {
                basketProduct = new BasketProduct()
                {
                    BasketId = basket.GuId,
                    CourseId = null,
                    EventsId = id,
                    Quantity = 1
                };
            }
            basket.BasketProducts.Add(basketProduct);
        }

        await _basketWriteRepository.SaveChangesAsync();
    }


    public Task DeleteBasketItem(Guid CartId, string AppuserId)
    {
        throw new NotImplementedException();
    }

 
    public Task DeleteBasktetAsyc(Guid id, string AppuserId)
    {
        throw new NotImplementedException();
    }

    public Task<int> getBasketCount(string AppuserId)
    {
        throw new NotImplementedException();
    }

    public Task<List<BasketProduct>> GetBasketProduct(string AppuserId)
    {
        throw new NotImplementedException();
    }
}
