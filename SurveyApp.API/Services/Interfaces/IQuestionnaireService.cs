using SurveyApp.DtoModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurveyApp.API.Services.Interfaces
{
	public interface IQuestionnaireService
	{
		Task<IEnumerable<PublicQuestionnaireDto>> Get(FilterParams filterParams);
		PublicQuestionnaireDto Create(CreateQuestionnaireDto questionnaire, string currentUserId);
	}
}
