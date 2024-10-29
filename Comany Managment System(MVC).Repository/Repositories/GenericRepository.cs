using Comany_Managment_System_MVC_.Core.Interfaces;
using Comany_Managment_System_MVC_.Core.Spercification;
using Comany_Managment_System_MVC_.Core.Models;
using Comany_Managment_System_MVC_.Repository.Data;
using Comany_Managment_System_MVC_.Repository.Specification;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Comany_Managment_System_MVC_.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
      
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAll()
            => await _context.Set<T>()
            .AsNoTracking()
            .ToListAsync();

        public async Task<IEnumerable<T>> GetAllWithSpecification(ISpecifications<T> specifications)
            => await ApplaySpecification(specifications).AsNoTracking().ToListAsync();

        public async Task<T?> Find(Expression<Func<T, bool>> criteria)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();
            return await query.SingleOrDefaultAsync(criteria);
        }

        public async Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> criteria)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();
            return await query.Where(criteria).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllWithTracking(Expression<Func<T, bool>> criteria)
        {
            IQueryable<T> query = _context.Set<T>();
            return await query.Where(criteria).ToListAsync();
        }

        public async Task<T?> FindWithSpecification(Expression<Func<T, bool>> criteria, 
            ISpecifications<T> specifications)
        {
            IQueryable<T> query = ApplaySpecification(specifications).AsNoTracking();
            return await query.SingleOrDefaultAsync(criteria);
        }

        public async Task<T?> FindWithSpecificationWithTrack(Expression<Func<T, bool>> criteria, 
            ISpecifications<T> specifications)
        {
            IQueryable<T> query = SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), specifications);
            return await query.SingleOrDefaultAsync(criteria);
        }
        public async Task<IEnumerable<T>> FindAllWithSpecificationWithTrack(Expression<Func<T, bool>> criteria, 
            ISpecifications<T> specifications)
        {
            IQueryable<T> query = SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), specifications);
            return await query.Where(criteria).ToListAsync();
        }

        public async Task Create(T model)
        {
            _context.Set<T>().Add(model);
            await Save();
        }

        public async Task<int> Update(T model)
        {
            _context.Set<T>().Update(model);
            return await Save();
        }

        public async Task<int> Delete(T model)
        {
            _context.Set<T>().Remove(model);
            return await Save();
        }

        private async Task<int> Save()
            => await _context.SaveChangesAsync();

        private IQueryable<T> ApplaySpecification(ISpecifications<T> specifications)
            => SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsNoTracking(), specifications);
    }
}
