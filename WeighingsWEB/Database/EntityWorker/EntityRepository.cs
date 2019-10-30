using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace WeighingsWEB.Database.EntityWorker
{
	public class EntityRepository<T> : IEntityRepository<T> where T : class
	{

		private DbContext databaseContext;
		private DbSet<T> dbSet;

		public EntityRepository(DbContext databaseContext)
		{
			this.databaseContext = databaseContext;
			dbSet = databaseContext.Set<T>();
		}

		public void Create(T t)
		{
			dbSet.Add(t);
			databaseContext.SaveChanges();
		}

		public T FindById(int Id)
		{
			return dbSet.Find(Id);
		}

		public IEnumerable<T> Get()
		{
			return dbSet.AsNoTracking().ToList();
		}

		public IQueryable<T> GetQueryable()
		{
			return dbSet.AsNoTracking();
		}

		public IEnumerable<T> Get(Func<T, bool> condition)
		{
			return dbSet.AsNoTracking().Where(condition).ToList();
		}

		public int Count()
		{
			return dbSet.Count();
		}

		public void Remove(T t)
		{
			dbSet.Remove(t);
			databaseContext.SaveChanges();
		}

		public void Update(T t)
		{
			databaseContext.Entry(t).State = EntityState.Modified;
			databaseContext.SaveChanges();
		}

	}
}
