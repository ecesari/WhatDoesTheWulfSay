using WhatDoesTheWulfSay.Domain.Entities;
using WhatDoesTheWulfSay.Domain.Specifications;
using System.Linq.Expressions;

namespace WhatDoesTheWulfSay.Domain.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
	{
		Task<IReadOnlyList<T>> GetAllAsync();	
		Task<IReadOnlyList<T>> GetAsync(IBaseSpecification<T> spec);
		Task<T> GetByIdAsync(int id);
		Task<T> AddAsync(T entity);
		Task<List<T>> AddBulkAsync(List<T> entities);
		Task UpdateAsync(T entity);
		Task DeleteAsync(T entity);
	}
}
