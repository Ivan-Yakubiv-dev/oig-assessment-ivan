using SurveyApp.Common.Constants;
using SurveyApp.Common.Enums;
using SurveyApp.Domain.Entities;
using System;
using System.Linq;

namespace SurveyApp.API.DAL
{
	public class DefaultDataSeeder
	{
		private readonly SurveyDbContext _dbContext;

		public DefaultDataSeeder(SurveyDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public void Seed()
		{
			Console.WriteLine("DefaultDataSeeder.cs - Seed() STARTING...");

			SeedUsers();

			SeedQuestionnaires();

			Console.WriteLine("DefaultDataSeeder.cs - Seed() FINISHED.");
		}

		public void SeedUsers()
		{
			Console.WriteLine("DefaultDataSeeder.cs - SeedUsers()...");

			if (_dbContext.Users.Any(u => u.Id == MockConsts.MockAdminId))
			{
				return;
			}

			_dbContext.Users.Add(new User
			{
				Id = MockConsts.MockAdminId,
				Type = UserType.Admin
			});

			_dbContext.Users.Add(new User
			{
				Id = MockConsts.MockUserId,
				Type = UserType.User
			});

			_dbContext.SaveChanges();
		}

		public void SeedQuestionnaires()
		{
			Console.WriteLine("DefaultDataSeeder.cs - SeedQuestionnaires()...");

			if (_dbContext.Questionnaires.Any())
			{
				return;
			}

			_dbContext.Questionnaires.Add(new Questionnaire
			{
				Name = "How are you?",
				Status = SurveyStatus.Concept,
				OwnerId = MockConsts.MockAdminId
			});

			_dbContext.SaveChanges();
		}
	}
}
