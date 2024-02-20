using System.Text.Json.Serialization;
using Echooling.Aplication.Abstraction.Repository;
using Echooling.Aplication.Abstraction.Repository.AppUserEventRepository;
using Echooling.Aplication.Abstraction.Repository.Basket;
using Echooling.Aplication.Abstraction.Repository.BasketProduct;
using Echooling.Aplication.Abstraction.Repository.CourseCategory;
using Echooling.Aplication.Abstraction.Repository.CourseReviewRepositories;
using Echooling.Aplication.Abstraction.Repository.Couse;
using Echooling.Aplication.Abstraction.Repository.EventCategoryRepository;
using Echooling.Aplication.Abstraction.Repository.EventRepositories;
using Echooling.Aplication.Abstraction.Repository.EventsStaff;
using Echooling.Aplication.Abstraction.Repository.LoggerRepositories;
using Echooling.Aplication.Abstraction.Repository.SliderRepositories;
using Echooling.Aplication.Abstraction.Repository.StaffRepositories;
using Echooling.Aplication.Abstraction.Repository.TeacherCourses;
using Echooling.Aplication.Abstraction.Repository.TeacherRepositories;
using Echooling.Aplication.Abstraction.Repository.VideoRepositories;
using Echooling.Aplication.Abstraction.Services;
using Echooling.Aplication.Valudators.SliderValudators;
using Echooling.Persistance.Contexts;
using Echooling.Persistance.Helper;
using Echooling.Persistance.Implementations.Repositories.AppUserEventsRepositories;
using Echooling.Persistance.Implementations.Repositories.BasketProductRepositories;
using Echooling.Persistance.Implementations.Repositories.BasketReposiyories;
using Echooling.Persistance.Implementations.Repositories.CategoryRepositories;
using Echooling.Persistance.Implementations.Repositories.CourseRepositories;
using Echooling.Persistance.Implementations.Repositories.CourseReviewRepositories;
using Echooling.Persistance.Implementations.Repositories.EventCategoryRepsitory;
using Echooling.Persistance.Implementations.Repositories.EventRepositories;
using Echooling.Persistance.Implementations.Repositories.LoggerRepositories;
using Echooling.Persistance.Implementations.Repositories.SliderRepositories;
using Echooling.Persistance.Implementations.Repositories.StaffEventsRepositories;
using Echooling.Persistance.Implementations.Repositories.StaffRepository;
using Echooling.Persistance.Implementations.Repositories.TeaceherCoursesRepository;
using Echooling.Persistance.Implementations.Repositories.TeacherRepositories;
using Echooling.Persistance.Implementations.Repositories.VideoContentRepositories;
using Echooling.Persistance.Implementations.Services;
using Echooling.Persistance.MapperProfile;
using Ecooling.Domain.Entites;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Echooling.Persistance;
public static class ServiceRegistration
{
    public static void addPersistanceServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        //fluet valudations
        services.AddControllers()
          .AddJsonOptions(options =>
          {
              options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
          });
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<SliderCreateDtoValudator>();
        //auto mapper
        services.AddAutoMapper(typeof(SliderProfile).Assembly);

        //dbContext
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(Configuration.ConnetionString);
        });
        //read write repos
        services.AddReadRepositories();
        services.AddWriteRepositories();

        //services
        services.AddScoped<ISliderService, SliderServices>();
        services.AddScoped<ITeacherService, TeacherServices>();
        services.AddScoped<IStaffService, StaffService>();
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IStaffEventsService, EventStaffServices>();
        services.AddScoped<IEventsCategoryService, EventCategoryService>();
        services.AddScoped<IAppUserEventService, AppuserEventService>();
        services.AddScoped<ICourseCategoryService, CourseCAtegoryServices>();
        services.AddScoped<ITeacherCourses, TeacherCoursesService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IVideoContentService, VideoService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<ICourseReviewServices, CourseReviewService>();
        services.AddScoped<IBasketService, BasketService>();
        services.AddScoped<IBasketProductService, BasketProductService>();
        services.AddScoped<ILoggerService, LoggerService>();
        services.AddScoped<IInfoService, InfoService>();
        //Idenitity
        services.AddIdentity<AppUser, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireDigit = true;
            options.Password.RequireUppercase = true;
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        })
        .AddDefaultTokenProviders()
        .AddEntityFrameworkStores<AppDbContext>();
        services.Configure<DataProtectionTokenProviderOptions>(opt =>
        opt.TokenLifespan = TimeSpan.FromHours(2));
    }
    private static void AddReadRepositories(this IServiceCollection services)
    {
        services.AddScoped<ISliderReadRepository, SliderReadRepository>();
        services.AddScoped<ITeacherReadRepository, TeacherReadRepository>();
        services.AddScoped<IStaffReadRepository, StaffReadRepository>();
        services.AddScoped<IEventReadRepository, EventReadRepository>();
        services.AddScoped<IEventStaffWriteRepository, StaffEventWriteRepository>();
        services.AddScoped<IEventCategoryReadRepository, EventCategoryReadReposiory>();
        services.AddScoped<IAppuserEventReadRopository, AppUserEventsReadRepository>();
        services.AddScoped<ITeacherCoursesReadRepository, TeacherCoursesReadRepository>();
        services.AddScoped<ICourseCategoryReadRepository, CategoryReadRepository>();
        services.AddScoped<ICourseReadRepository, CourseReadRepository>();
        services.AddScoped<IVideoContentReadRepository, VideoContentReadRepository>();
        services.AddScoped<ICourseReviewReadRepository, CourseReviewReadRepository>();
        services.AddScoped<IBasketProductReadRepository, BasketProductReadRepository>();
        services.AddScoped<IBasketReadRepository, BasketReadRepository>();
        services.AddScoped<ILoggerReadRepository, LoggerReadRepository>();

    }
    private static void AddWriteRepositories(this IServiceCollection services)
    {
        services.AddScoped<ISliderWriteRepository, SliderWriteRepository>();
        services.AddScoped<ITeacherWriteRepository, TeacherWriteRepositories>();
        services.AddScoped<IStaffWriteRepository, StaffWriteRepository>();
        services.AddScoped<IEventWriteRepository, EventWriteRepository>();
        services.AddScoped<IEventStaffReadRepository, StaffEventsReadRepository>();
        services.AddScoped<IEventCategoryWriteRepository, EventCategoryWriteRepository>();
        services.AddScoped<IAppuserEventWriteRepository, AppUserEventWriteRepostitory>();
        services.AddScoped<ICourseCategoryWriteRepository, CategoryWriteRepository>();
        services.AddScoped<ICourseWriteRepository, CourseWriteRepository>();
        services.AddScoped<ITeacherCoursesWriteRepository, TeacherCourseWriteRepository>();
        services.AddScoped<IVideoContentWriteRepository, VideoContentWriteRepository>();
        services.AddScoped<ICourseReviewWriteRepository, CourseReviewWriteRepository>();
        services.AddScoped<IBasketProductWriteRepository, BasketProductWriteRepository>();
        services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();
        services.AddScoped<ILoggerWriteRepository, LoggerWriteRepository>();
    }
}