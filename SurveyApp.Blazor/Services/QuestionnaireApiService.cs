using Newtonsoft.Json;
using SurveyApp.DtoModels;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Blazor.Services
{
	public class QuestionnaireApiService
	{
		public const string QuestionnaireApiRoute = "Api/Questionnaire";

		private readonly HttpClient _httpClient;

		public QuestionnaireApiService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<PublicQuestionnaireDto[]> GetQuestionnaires(FilterParams filterParams)
		{
			var questionnairesResponse = await _httpClient.GetAsync($"{QuestionnaireApiRoute}{filterParams.GenereateQueryParams()}")
				.ConfigureAwait(false);

			return await HandleHttpResponse<PublicQuestionnaireDto[]>(questionnairesResponse)
				.ConfigureAwait(false);
		}

		public async Task<PublicQuestionnaireDto> GetQuestionnaire(string questionnaireIdRouteParam)
		{
			var questionnaireResponse = await _httpClient.GetAsync($"{QuestionnaireApiRoute}/{questionnaireIdRouteParam}")
				.ConfigureAwait(false);

			return await HandleHttpResponse<PublicQuestionnaireDto>(questionnaireResponse)
				.ConfigureAwait(false);
		}

		public async Task<PublicQuestionnaireDto> CreateQuestionnaire(CreateQuestionnaireDto questionnaireInput)
		{
			var stringContent = new StringContent(
				JsonConvert.SerializeObject(questionnaireInput),
				Encoding.UTF8,
				"application/json");

			var questionnaireResponse = await _httpClient.PostAsync(QuestionnaireApiRoute, stringContent)
				.ConfigureAwait(false);

			return await HandleHttpResponse<PublicQuestionnaireDto>(questionnaireResponse)
				.ConfigureAwait(false);
		}

		public async Task<PublicQuestionnaireDto> ScheduleQuestionnaire(UpdateQuestionnaireTimeRangeDto questionnaireInput)
		{
			var stringContent = new StringContent(
				JsonConvert.SerializeObject(questionnaireInput),
				Encoding.UTF8,
				"application/json");

			var questionnaireResponse = await _httpClient.PutAsync($"{QuestionnaireApiRoute}/Schedule", stringContent)
				.ConfigureAwait(false);

			return await HandleHttpResponse<PublicQuestionnaireDto>(questionnaireResponse)
				.ConfigureAwait(false);
		}

		public async Task<PublicQuestionnaireDto> RescheduleQuestionnaire(UpdateQuestionnaireTimeRangeDto questionnaireInput)
		{
			var stringContent = new StringContent(
				JsonConvert.SerializeObject(questionnaireInput),
				Encoding.UTF8,
				"application/json");

			var questionnaireResponse = await _httpClient.PutAsync($"{QuestionnaireApiRoute}/Reschedule", stringContent)
				.ConfigureAwait(false);

			return await HandleHttpResponse<PublicQuestionnaireDto>(questionnaireResponse)
				.ConfigureAwait(false);
		}

		public async Task<PublicQuestionnaireDto> CloseQuestionnaire(string questionnaireIdRouteParam)
		{
			var questionnaireResponse = await _httpClient.PutAsync($"{QuestionnaireApiRoute}/{questionnaireIdRouteParam}/Close", null)
				.ConfigureAwait(false);

			return await HandleHttpResponse<PublicQuestionnaireDto>(questionnaireResponse)
				.ConfigureAwait(false);
		}

		private async Task<T> HandleHttpResponse<T>(HttpResponseMessage responseMessage)
		{
			string responseContent = await responseMessage.Content.ReadAsStringAsync()
				.ConfigureAwait(false);

			return JsonConvert.DeserializeObject<T>(responseContent);
		}
	}
}