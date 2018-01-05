using System.Collections.Generic;
using System.Linq;
using AmazonSqsQueuesManager.DataAccess;
using AmazonSqsQueuesManager.Domain;

namespace AmazonSqsQueuesManager.DAL.EF
{
	public class TraceRecordsRepository : ITraceRecordsRepository
	{
		public IEnumerable<TracingRecord> GetRecords()
		{
			using (var traceRecordsContext = new TraceRecordsContext())
			{
				var result = (from r in traceRecordsContext.TracingRecords
							  //orderby r.TimeStamp descending
							  select r).ToList();

				return result;
			}
		}

		public TracingRecord GetLastAddedRecord()
		{
			using (var traceRecordsContext = new TraceRecordsContext())
			{
				var result = (from r in traceRecordsContext.TracingRecords
					orderby r.TimeStamp descending
					select r).FirstOrDefault();

				return result;
			}
		}

		public void AddRecord(TracingRecord tracingRecord)
		{
			using (var traceRecordsContext = new TraceRecordsContext())
			{
				traceRecordsContext.TracingRecords.Add(tracingRecord);
				traceRecordsContext.SaveChanges();
			}
		}

		public void DeleteAllRecords()
		{
			using (var traceRecordsContext = new TraceRecordsContext())
			{
				var allRecords = traceRecordsContext.TracingRecords;

				traceRecordsContext.TracingRecords.RemoveRange(allRecords);
				traceRecordsContext.SaveChanges();
			}
		}
	}
}