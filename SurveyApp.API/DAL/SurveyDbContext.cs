using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SurveyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SurveyApp.API.DAL
{
	public class SurveyDbContext : DbContext
	{
		public DbSet<Questionnaire> Questionnaires { get; set; }
		public DbSet<QuestionnaireItem> QuestionnaireItems { get; set; }
		public DbSet<QuestionnaireSubmission> QuestionnaireSubmissions { get; set; }
		public DbSet<QuestionnaireAnswer> QuestionnaireAnswers { get; set; }
		public DbSet<User> Users { get; set; }

		public SurveyDbContext(DbContextOptions<SurveyDbContext> dbOptions)
			: base(dbOptions)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.UseSerialColumns();

			ConfigureCreatedDateProperty(modelBuilder);

			modelBuilder.HasDefaultSchema("public");
			base.OnModelCreating(modelBuilder);
		}

		public override int SaveChanges()
		{
			ResolveChangeTracking();

			return base.SaveChanges();
		}

		public override int SaveChanges(bool acceptAllChangesOnSuccess)
		{
			ResolveChangeTracking();

			return base.SaveChanges(acceptAllChangesOnSuccess);
		}

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			ResolveChangeTracking();

			return await base.SaveChangesAsync(cancellationToken)
				.ConfigureAwait(true);
		}

		public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
		{
			ResolveChangeTracking();

			return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken)
				.ConfigureAwait(true);
		}

		private void ResolveChangeTracking()
		{
			var entries = ChangeTracker
				.Entries()
				.Where(e => (e.Entity is SABaseEntity) && ((e.State == EntityState.Added) || (e.State == EntityState.Modified)));

			foreach (var entityEntry in entries)
			{
				var trackableEntity = (SABaseEntity)entityEntry.Entity;

				// NOTE: Automatically set current UTC time into "CreatedDateUtc" field when DB entity is inserted
				if (entityEntry.State == EntityState.Added)
				{
					trackableEntity.CreatedDateUtc = DateTime.UtcNow;
					trackableEntity.LastModifiedDateUtc = null;
				}
				// NOTE: Automatically set current UTC time into "LastModifiedDateUtc" field when DB entity is updated
				else if (entityEntry.State == EntityState.Modified)
				{
					var lastModifiedProperty = Entry(trackableEntity).Property(nameof(trackableEntity.LastModifiedDateUtc));

					if (lastModifiedProperty.OriginalValue == lastModifiedProperty.CurrentValue)
					{
						trackableEntity.LastModifiedDateUtc = DateTime.UtcNow;
					}
				}
			}
		}

		private void ConfigureCreatedDateProperty(ModelBuilder modelBuilder)
		{
			IEnumerable<IMutableEntityType> entityTypes = modelBuilder.Model.GetEntityTypes();

			foreach (var type in entityTypes)
			{
				// NOTE: Restrict changing value of CreatedDateUtc field when existing database entity is updated
				IMutableProperty createdDateUtcProperty = type.GetProperties()
					.FirstOrDefault(x => (x.PropertyInfo != null)
						? x.PropertyInfo.Name.Equals(nameof(SABaseEntity.CreatedDateUtc), StringComparison.OrdinalIgnoreCase)
						: false);

				if (createdDateUtcProperty != null)
				{
					createdDateUtcProperty.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
				}
			}
		}
	}
}
