namespace SurveyApp.Common.Enums
{
	public enum UserType : byte
	{
		Unknown = 0,

		/// <summary>
		/// System user, has full access to the system, and can manage questionnairess
		/// </summary>
		Admin = 1,
		/// <summary>
		/// Regular user, has limited access to the system, and can fill out questionnaires
		/// </summary>
		User = 2
	}
}
