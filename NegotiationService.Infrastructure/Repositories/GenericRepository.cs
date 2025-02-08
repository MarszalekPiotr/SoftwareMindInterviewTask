using Microsoft.EntityFrameworkCore;
using NegotiationService.Application.Interfaces;
using NegotiationService.Domain.Entities;
using NegotiationService.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationService.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {     
        private readonly MainDbContext _context;
        public GenericRepository(MainDbContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            _context.Add(entity);
        }

        public void Delete(int id)
        {
            _context.Remove(id);    
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
           return await _context.Set<TEntity>().FirstOrDefaultAsync( e => e.Id == id);
        }

        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
    }
}
