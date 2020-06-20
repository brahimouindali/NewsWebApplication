using System;
using System.Text;

namespace Data.Access.Library.Interfaces
{
	public interface IUnitOfWork<T> where T : class
	{
		IGenericRepository<T> Entity { get; }
		void Save();
	}
}
