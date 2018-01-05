using System.Collections.Generic;
using System.Linq;
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace AmazonSqsQueuesManager.AwsSqs
{
	public class AwsClient
	{
		private const int WaitTimeSeconds = 20;

		private static volatile AwsClient _awsClient;
		private readonly IAmazonSQS _amazonSqs;
		private readonly string _queueUrl;

		private AwsClient()
		{
			_amazonSqs = AWSClientFactory.CreateAmazonSQSClient();
			QueueName = "TestQueue2";

			//Creating a queue
			var sqsRequest = new CreateQueueRequest { QueueName = QueueName };
			var createQueueResponse = _amazonSqs.CreateQueue(sqsRequest);			
			_queueUrl = createQueueResponse.QueueUrl;
		}

		public static AwsClient Instance
		{
			get
			{
				if (_awsClient == null)
				{
					if (_awsClient == null)
						_awsClient = new AwsClient();
				}

				return _awsClient;
			}
		}


		public string QueueName { get; private set; }

		public int Count()
		{
			var getQueueAttributes = new GetQueueAttributesRequest(_queueUrl, new List<string> {"ApproximateNumberOfMessages"});

			var getQueueAttrbutesResponse = _amazonSqs.GetQueueAttributes(getQueueAttributes);
			return getQueueAttrbutesResponse.ApproximateNumberOfMessages;
		}

		public void Enqueue(string message)
		{
			var sendMessageRequest = new SendMessageRequest
			{
				QueueUrl = _queueUrl,
				MessageBody = message
			};

			_amazonSqs.SendMessage(sendMessageRequest);
		}

		public string Dequeue()
		{
			//Receiving a message
			var receiveMessageRequest = new ReceiveMessageRequest {QueueUrl = _queueUrl, WaitTimeSeconds = WaitTimeSeconds};
			var message = _amazonSqs.ReceiveMessage(receiveMessageRequest).Messages.FirstOrDefault();

			if (message == null)
				return string.Empty;

			var messageRecieptHandle = message.ReceiptHandle;
			//Deleting a message
			var deleteRequest = new DeleteMessageRequest
			{
				QueueUrl = _queueUrl,
				ReceiptHandle = messageRecieptHandle
			};
			_amazonSqs.DeleteMessage(deleteRequest);

			return message.Body;
		}

		public void Clear()
		{
			_amazonSqs.PurgeQueue(_queueUrl);
		}
	}
}