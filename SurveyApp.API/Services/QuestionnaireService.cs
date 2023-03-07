using Microsoft.EntityFrameworkCore;
using SurveyApp.API.DAL;
using SurveyApp.API.Services.Interfaces;
using SurveyApp.DtoModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyApp.API.Services
{
	public class QuestionnaireService : IQuestionnaireService
	{
		protected readonly SurveyDbContext _dbContext;

		public QuestionnaireService(SurveyDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<PublicQuestionnaireDto>> Get(FilterParams filterParams)
		{
			var entities = await _dbContext.Questionnaires
				// NOTE: GenericFilter() method could be used here if it would be fully implemented and would meet requirements.
				//		 Otherwise - custom FilterQuestionnaires() method would be used here to build custom DB query.
				//.GenericFilter(filterParams)
				.FilterQuestionnaires(filterParams)
				.GenericSort(filterParams)
				.GenericPaginate(filterParams)
				.ToListAsync()
				.ConfigureAwait(false);

			return entities.Select(e => new PublicQuestionnaireDto(e));
		}
	}
}
