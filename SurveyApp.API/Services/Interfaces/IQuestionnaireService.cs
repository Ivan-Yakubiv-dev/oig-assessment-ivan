using SurveyApp.DtoModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurveyApp.API.Services.Interfaces
{
	public interface IQuestionnaireService
	{
		Task<IEnumerable<PublicQuestionnaireDto>> Get(FilterParams filterParams);
		Task<PublicQuestionnaireDto> Get(int questionnaireId);
		PublicQuestionnaireDto Create(CreateQuestionnaireDto questionnaireInput, string currentUserId);
		PublicQuestionnaireDto Schedule(UpdateQuestionnaireTimeRangeDto questionnaireInput, string currentUserId);
		PublicQuestionnaireDto Reschedule(UpdateQuestionnaireTimeRangeDto questionnaireInput, string currentUserId);
		PublicQuestionnaireDto Close(int questionnaireId, string currentUserId);
	}
}
