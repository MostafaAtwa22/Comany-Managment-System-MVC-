using Comany_Managment_System_MVC_.Core.Interfaces;
using Comany_Managment_System_MVC_.Models;
using Comany_Managment_System_MVC_.Repository.Data;
using Microsoft.EntityFrameworkCore;

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

        public async Task<T?> GetById(int id)
            => await _context.Set<T>()
            .AsNoTracking()
            .SingleOrDefaultAsync(i => i.Id == id);

        public async Task Create(T model)
        {
            _context.Set<T>().Add(model);
            await Save();
        }

        public async Task Update(T model)
        {
            _context.Set<T>().Update(model);
            _context.Entry(model).State = EntityState.Modified;
            await Save();
        }

        public async Task Delete(T model)
        {
            _context.Set<T>().Remove(model);
            await Save();
        }

        public async Task Save()
            => await _context.SaveChangesAsync();
    }
}
