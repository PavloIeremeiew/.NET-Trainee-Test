using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using NET_Trainee_Test_MVC.Models.Persistence;
using System.Linq.Expressions;

namespace NET_Trainee_Test_MVC.Models.Repositories.Realizations.Base
{
    public class RepositoryBase<T> : Interfaces.Base.IRepositoryBase<T>
        where T : class
    {
        private CsvDBContext _dbContext = null!;

        protected RepositoryBase(CsvDBContext context)
        {
            _dbContext = context;
        }

        public CsvDBContext DbContext { init => _dbContext = value; }

        public IQueryable<T> FindAll(Expression<Func<T, bool>>? predicate = default)
        {
            return GetQueryable(predicate).AsNoTracking();
        }

        public T Create(T entity)
        {
            return _dbContext.Set<T>().Add(entity).Entity;
        }

        public async Task<T> CreateAsync(T entity)
        {
            var tmp = await _dbContext.Set<T>().AddAsync(entity);
            return tmp.Entity;
        }

        public Task CreateRangeAsync(IEnumerable<T> items)
        {
            return _dbContext.Set<T>().AddRangeAsync(items);
        }
        public void CreateRange(IEnumerable<T> items)
        {
            _dbContext.Set<T>().AddRange(items);
        }

        public EntityEntry<T> Update(T entity)
        {
            return _dbContext.Set<T>().Update(entity);
        }

        public void UpdateRange(IEnumerable<T> items)
        {
            _dbContext.Set<T>().UpdateRange(items);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> items)
        {
            _dbContext.Set<T>().RemoveRange(items);
        }


        public IEnumerable<T> GetAll(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>,
                IIncludableQueryable<T, object>>? include = null)
        {
            return GetQueryable(predicate, include).ToList();
        }

        public IEnumerable<T>? GetAllWithSpec(params ISpecification<T>[] specs)
        {
            return  ApplySpecifications(specs)
                .ToList();
        }

        public T? GetFirstOrDefault(
            Expression<Func<T, bool>>? predicate = default,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default)
        {
            return GetQueryable(predicate, include).FirstOrDefault();
        }

        public T? GetFirstOrDefaultWithSpec(params ISpecification<T>[] specs)
        {
            return ApplySpecifications(specs)
                .FirstOrDefault();
        }

        private IQueryable<T> ApplySpecifications(params ISpecification<T>[] specs)
        {
            var query = _dbContext.Set<T>().AsQueryable().AsNoTracking();

            foreach (var spec in specs)
            {
                query = SpecificationEvaluator.Default.GetQuery(query, spec);
            }

            return query;
        }

        private IQueryable<T> GetQueryable(
            Expression<Func<T, bool>>? predicate = default,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default,
            Expression<Func<T, T>>? selector = default)
        {
            var query = _dbContext.Set<T>().AsNoTracking();

            if (include is not null)
            {
                query = include(query);
            }

            if (predicate is not null)
            {
                query = query.Where(predicate);
            }

            if (selector is not null)
            {
                query = query.Select(selector);
            }

            return query.AsNoTracking();
        }
    }

}