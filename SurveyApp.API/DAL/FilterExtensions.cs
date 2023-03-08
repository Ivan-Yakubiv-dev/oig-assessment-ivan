using Microsoft.AspNetCore.Http;
using SurveyApp.Common.Constants;
using SurveyApp.Common.Exceptions;
using SurveyApp.Domain.Entities;
using SurveyApp.DtoModels;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SurveyApp.API.DAL
{
	public static class FilterExtensions
	{
		public static IQueryable<T> GenericPaginate<T>(this IQueryable<T> entities, FilterParams filterParams)
			where T : SABaseEntity
		{
			return entities
				.Skip((filterParams.Page - 1) * filterParams.PageSize)
				.Take(filterParams.PageSize);
		}

		public static IQueryable<T> GenericSort<T>(this IQueryable<T> entities, FilterParams filterParams)
			where T : SABaseEntity
		{
			if (string.IsNullOrEmpty(filterParams.SortBy))
			{
				filterParams.SortBy = nameof(SABaseEntity.CreatedDateUtc);
			}

			PropertyInfo sortingProp = typeof(T).GetProperty(
				filterParams.SortBy,
				BindingFlags.SetProperty | BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

			if (sortingProp == null)
			{
				throw new SABadRequestException(
					$"Entities of type '{typeof(T).Name}' does not have property '{filterParams.SortBy}' to be sorted by.",
					ErrorConsts.WrongFilterParamsPublicError);
			}

			var parameter = Expression.Parameter(typeof(T), "x");
			Expression property = Expression.Property(parameter, sortingProp);
			var lambda = Expression.Lambda(property, parameter);
			var orderByMethod = filterParams.IfSortAsc
				? typeof(Queryable).GetMethods().First(x => x.Name == "OrderBy" && x.GetParameters().Length == 2)
				: typeof(Queryable).GetMethods().First(x => x.Name == "OrderByDescending" && x.GetParameters().Length == 2);
			var orderByGeneric = orderByMethod.MakeGenericMethod(typeof(T), property.Type);
			var sortedEntities = orderByGeneric.Invoke(null, new object[] { entities, lambda });

			return (IOrderedQueryable<T>)sortedEntities;
		}

		public static IQueryable<T> GenericFilter<T>(this IQueryable<T> entities, FilterParams filterParams)
			where T : SABaseEntity
		{
			if (string.IsNullOrWhiteSpace(filterParams.FilterTerm))
			{
				return entities;
			}

			// NOTE: EF lambda expression can be implemented here to support filtering for any entity (by using "Like" operator on strings, etc.).
			//		 But as long as it is not always possible to achieve business goals with generic solution, I prefer
			//		 to build custom filtering rules on demand in either specific typed Service/Repository or stored procedure on DB side.

			return entities;
		}

		public static IQueryable<Questionnaire> FilterQuestionnaires(this IQueryable<Questionnaire> entities, FilterParams filterParams)
		{
			if (string.IsNullOrWhiteSpace(filterParams.FilterTerm))
			{
				return entities;
			}

			return entities.Where(e => e.Name.ToUpper().Contains(filterParams.FilterTerm.ToUpper()));
		}
	}
}
