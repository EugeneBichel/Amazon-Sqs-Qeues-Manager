using System.Collections.Generic;
using AmazonSqsQueuesManager.Domain;
using Resource = AmazonSqsQueuesManager.Business.Models;

namespace AmazonSqsQueuesManager.Business.Services
{
	public interface ITraceSqsRecordsService
	{
		Resource.TracingRecord GetLastAddedRecord();
		IEnumerable<Resource.TracingRecord> GetAllRecords();
		void Add(string queueName, ActionType actionType);
		void DeleteAllRecords();
	}
}