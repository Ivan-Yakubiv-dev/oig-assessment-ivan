using System;

namespace SurveyApp.Domain.Entities
{
	public abstract class SABaseEntity
	{
		public DateTime CreatedDateUtc { get; set; }
		public DateTime? LastModifiedDateUtc { get; set; }
	}
}
