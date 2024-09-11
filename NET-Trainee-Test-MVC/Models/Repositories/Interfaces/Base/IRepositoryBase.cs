using Ardalis.Specification;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace NET_Trainee_Test_MVC.Models.Repositories.Interfaces.Base
{
    public interface IRepositoryBase<T>
    where T : class
    {
        IQueryable<T> FindAll(Expression<Func<T, bool>>? predicate = default);
        T Create(T entity);
        void CreateRange(IEnumerable<T> items);

        EntityEntry<T> Update(T entity);

        public void UpdateRange(IEnumerable<T> items);

        void Delete(T entity);

        void DeleteRange(IEnumerable<T> items);

        IEnumerable<T> GetAll(
           Expression<Func<T, bool>>? predicate = default,
           Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default);
        IEnumerable<T>? GetAllWithSpec(params ISpecification<T>[] specs);

        T? GetFirstOrDefault(
            Expression<Func<T, bool>>? predicate = default,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default);
         T? GetFirstOrDefaultWithSpec(params ISpecification<T>[] specs);
    }
}
