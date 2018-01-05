using System;

namespace AmazonSqsQueuesManager.Business.Models
{
	public class TracingRecord
	{
		public string QueueName { get; set; }
		public string ActionType { get; set; }
		public string TimeStamp { get; set; }
	}
}