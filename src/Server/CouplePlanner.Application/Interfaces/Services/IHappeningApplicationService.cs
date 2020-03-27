using System.Collections.Generic;

namespace CouplePlanner.Application.Interfaces.Services
{
	public interface IHappeningApplicationService<T, TDomain> : IApplicationService<T, TDomain>
	{
		IEnumerable<T> GetUpcoming(int take);
	}
}
