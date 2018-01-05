using System.Collections.Generic;
using AmazonSqsQueuesManager.Domain;

namespace AmazonSqsQueuesManager.DataAccess
{
	public interface ITraceRecordsRepository
	{
		IEnumerable<TracingRecord> GetRecords();
		TracingRecord GetLastAddedRecord();
		void AddRecord(TracingRecord tracingRecord);
		void DeleteAllRecords();
	}
}