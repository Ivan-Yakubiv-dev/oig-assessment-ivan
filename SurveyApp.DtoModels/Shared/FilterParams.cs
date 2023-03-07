﻿namespace SurveyApp.DtoModels
{
	public class FilterParams
	{
		public int Page { get; set; } = 1;
		public int PageSize { get; set; } = 10;

		public string SortBy { get; set; }
		public bool IfSortAsc { get; set; }

		public string FilterTerm { get; set; }
	}
}
