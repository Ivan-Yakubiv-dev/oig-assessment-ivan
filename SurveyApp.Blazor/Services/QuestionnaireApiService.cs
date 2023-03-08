using Newtonsoft.Json;
using SurveyApp.DtoModels;
using System.Net.Http;
using System.Threading.Tasks;

namespace SurveyApp.Blazor.Services
{
	public class QuestionnaireApiService
	{
		private readonly HttpClient _httpClient;

		public QuestionnaireApiService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<PublicQuestionnaireDto[]> GetQuestionnaires()
		{
			var questionnairesResponse = await _httpClient.GetAsync(string.Empty)
				.ConfigureAwait(false);
			
			string responseContent = await questionnairesResponse.Content.ReadAsStringAsync()
				.ConfigureAwait(false);

			return JsonConvert.DeserializeObject<PublicQuestionnaireDto[]>(responseContent);
		}
	}
}