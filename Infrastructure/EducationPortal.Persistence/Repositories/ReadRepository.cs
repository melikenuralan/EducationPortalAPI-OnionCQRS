using EducationPortal.Application.Abstractions.Repositories.Generic;
using EducationPortal.Domain.Entities;
using EducationPortal.Domain.Entities.Common;
using EducationPortal.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EducationPortal.Persistance.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly EduPortalDbContext _context;
        public ReadRepository(EduPortalDbContext context)
        {
            _context = context;
        }
        public DbSet<T> Table => _context.Set<T>();
        public IQueryable<T> GetAll(bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }
        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.Where(method);
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = Table.AsNoTracking();
            return await query.FirstOrDefaultAsync(method);

        }
        public async Task<T> GetByIdAsync(int id, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (tracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(data => data.Id == id);
        }
        public async Task<IEnumerable<T>> GetAllAsync(bool tracking = true)
        {
            IQueryable<T> query = Table.AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            return await query.ToListAsync();
        }

    }
}

