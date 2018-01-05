using System;
using System.Collections.Generic;
using System.Linq;
using AmazonSqsQueuesManager.Business.Mapping;
using AmazonSqsQueuesManager.DataAccess;
using Resource = AmazonSqsQueuesManager.Business.Models;
using Model = AmazonSqsQueuesManager.Domain;


namespace AmazonSqsQueuesManager.Business.Services
{
	public class TraceSqsRecordsService : ITraceSqsRecordsService
	{
		private readonly ITraceRecordsRepository _traceRecordsRepository;

		public TraceSqsRecordsService(ITraceRecordsRepository traceRecordsRepository)
		{
			_traceRecordsRepository = traceRecordsRepository;
		}

		public Resource.TracingRecord GetLastAddedRecord()
		{
			var model = _traceRecordsRepository.GetLastAddedRecord();
			var resource = TraceRecordMapper.MapModelToResource(model);

			return resource;
		}

		public IEnumerable<Resource.TracingRecord> GetAllRecords()
		{
			var models = _traceRecordsRepository.GetRecords();

			return models.Select(TraceRecordMapper.MapModelToResource).ToList();
		}

		public void Add(string queueName, Model.ActionType actionType)
		{
			if(string.IsNullOrWhiteSpace(queueName))
				throw new ArgumentNullException("queueName");

			var tracingRecord = new Model.TracingRecord
			{
				ActionType = actionType,
				QueueName = queueName,
				TimeStamp = DateTime.Now
			};

			_traceRecordsRepository.AddRecord(tracingRecord);
		}

		public void DeleteAllRecords()
		{
			_traceRecordsRepository.DeleteAllRecords();
		}
	}
}