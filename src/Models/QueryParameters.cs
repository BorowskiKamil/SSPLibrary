namespace SSPLibrary.Models
{
	public class QueryParameters
	{

		public PagingParameters PagingParameters { get; set; }

		public string ActionName { get; set; }

		public QueryParameters()
		{
			PagingParameters = new PagingParameters();
		}

	}
}