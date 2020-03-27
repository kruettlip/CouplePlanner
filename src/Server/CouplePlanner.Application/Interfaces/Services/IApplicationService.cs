using System;
using System.Collections.Generic;

namespace CouplePlanner.Application.Interfaces.Services
{
	public interface IApplicationService<T, TDomain>
	{
		IEnumerable<T> GetAll();

		Guid Create(T newEntity);

		void Delete(Guid id);
	}
}
