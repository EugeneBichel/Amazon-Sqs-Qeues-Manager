using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using AmazonSqsQueuesManager.DataAccess.Exceptions;
using AmazonSqsQueuesManager.Domain;

namespace AmazonSqsQueuesManager.DAL.EF
{
	public class TraceRecordsContext : DbContext
	{
		public TraceRecordsContext()
			: base("TracingContext")
		{
			Configuration.LazyLoadingEnabled = false;
		}

		public DbSet<TracingRecord> TracingRecords { get; set; }

		public override int SaveChanges()
		{
			int rowsCount;

			try
			{
				rowsCount = base.SaveChanges();
			}
			catch (DbUpdateConcurrencyException ex)
			{
				throw new DataAccessConcurrencyException(ex);
			}
			catch (DbUpdateException ex)
			{
				throw new DataAccessUpdateException(ex.Message, ex);
			}
			catch (DbEntityValidationException ex)
			{
				throw new DataAccessValidationException(ex);
			}
			catch (Exception ex)
			{
				throw new DataAccessException(ex.Message, ex);
			}

			return rowsCount;
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<TracingRecord>()
				.HasKey(p => p.TracingRecordId);
			modelBuilder.Entity<TracingRecord>()
				.Property(p => p.TracingRecordId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
			modelBuilder.Entity<TracingRecord>()
				.ToTable("TraceRecords");
		}
	}
}