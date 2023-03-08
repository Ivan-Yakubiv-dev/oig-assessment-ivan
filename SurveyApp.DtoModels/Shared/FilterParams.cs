namespace SurveyApp.DtoModels
{
	public class FilterParams
	{
		public int Page { get; set; } = 1;
		public int PageSize { get; set; } = 10;

		public string SortBy { get; set; }
		public bool IfSortAsc { get; set; }

		public string FilterTerm { get; set; }

		public string GenereateQueryParams()
		{
			string queryParams = $"?page={Page}&pageSize={PageSize}";

			if (!string.IsNullOrEmpty(SortBy))
			{
				queryParams += $"&sortBy={SortBy}";
			}

			if (!string.IsNullOrEmpty(SortBy))
			{
				queryParams += $"&ifSortAsc={IfSortAsc}";
			}

			if (!string.IsNullOrWhiteSpace(FilterTerm))
			{
				queryParams += $"&filterTerm={FilterTerm}";
			}

			return queryParams;
		}
	}
}
