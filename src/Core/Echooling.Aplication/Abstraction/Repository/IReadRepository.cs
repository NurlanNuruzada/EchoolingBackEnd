using System.Linq.Expressions;
using Ecooling.Domain.Entites;

namespace Echooling.Aplication.Abstraction.Repository
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        IQueryable<T> GetAll(bool IsTracking = true, params string[] includes);
        IQueryable<T> GetAllExpression(Expression<Func<T, bool>> expression, int take, int Skip, bool IsTracking = true, params string[] includes);
        IQueryable<T> GetAllExpressionOrderBy(Expression<Func<T, bool>> expression,
                                              int take,
                                              int Skip,
                                              Expression<Func<T, object>> expressionOrder,
                                              bool IsOrder = true,
                                              bool IsTracking = true,
                                              params string[] includes);
        Task<T?> GetByIdAsync(Guid Id);
        Task<T?> GetByExpressionAsync(Expression<Func<T, bool>> expression, bool IsTracking = true);
    }
}
