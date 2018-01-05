using System;

namespace AmazonSqsQueuesManager.Domain
{
    public class TracingRecord
    {
		public int TracingRecordId { get; set; }
		public string QueueName { get; set; }
		public ActionType ActionType { get; set; }
		public DateTime TimeStamp { get; set; }
    }
}