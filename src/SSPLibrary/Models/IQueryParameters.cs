using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSPLibrary.Models
{
	public interface IQueryParameters
	{

		PagingParameters PagingParameters { get; set; }

		string ActionName { get; set; }

		void ApplyQueryParameters(
				string sortParams,
				string searchOptions);
		
	}
}