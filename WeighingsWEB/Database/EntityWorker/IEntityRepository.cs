using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeighingsWEB.Database.EntityWorker
{
	public interface IEntityRepository<T>
	{ 
		void Create(T t);
		T FindById(int id);
		IEnumerable<T> Get();
		IEnumerable<T> Get(Func<T, bool> condition);
		void Remove(T t);
		void Update(T t);
		IQueryable<T> GetQueryable();
		int Count();


	}
}
