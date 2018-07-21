using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSPLibrary.Models
{
	public interface IQueryParameters
	{

		PagingParameters PagingParameters { get; set; }

		string ActionName { get; set; }

		IEnumerable<ValidationResult> ApplyQueryParameters(string sortParams, ValidationContext validationContext);
		
	}
}