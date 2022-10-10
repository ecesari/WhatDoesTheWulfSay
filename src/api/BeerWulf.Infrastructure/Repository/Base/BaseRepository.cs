using EverybodyCodes.Domain.Entities;
using EverybodyCodes.Domain.Repositories;
using EverybodyCodes.Domain.Specifications;
using EverybodyCodes.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;

namespace EverybodyCodes.Infrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
	{
		protected readonly CameraDbContext _context;

		public BaseRepository(CameraDbContext context)
		{
			_context = context;
		}

		public async Task<T> AddAsync(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
			await _context.SaveChangesAsync();
			return entity;
		}

		public async Task<List<T>> AddBulkAsync(List<T> entities)
		{
			await _context.Set<T>().AddRangeAsync(entities);
			await _context.SaveChangesAsync();
			return entities;
		}

		public async Task DeleteAsync(T entity)
		{
			_context.Set<T>().Remove(entity);
			await _context.SaveChangesAsync();
		}


		public async Task<IReadOnlyList<T>> GetAllAsync()
		{
			return await _context.Set<T>().ToListAsync();
		}

		public async Task<IReadOnlyList<T>> GetAsync(IBaseSpecification<T> spec)
		{
			return await ApplySpecification(spec).ToListAsync();

		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		public async Task UpdateAsync(T entity)
		{
			_context.Entry(entity).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}

		private IQueryable<T> ApplySpecification(IBaseSpecification<T> spec)
		{
			return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
		}

	}
}
