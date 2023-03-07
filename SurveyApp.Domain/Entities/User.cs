using SurveyApp.Common.Enums;

namespace SurveyApp.Domain.Entities
{
	public class User : SABaseEntity
	{
		public string Id { get; set; }

		public UserType Type { get; set; }
		// NOTE: The topic User is interested in, or topic which is being managed by Admin.
		//		 Might be used to form target groups for questionnaires.
		//		 Ideally would be implemented as separate independant table with many-to-many connection.
		public string? Topic { get; set; }
	}
}
