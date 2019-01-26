using System;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using AmazonSqsQueuesManager.AwsSqs;
using AmazonSqsQueuesManager.Business.Services;
using AmazonSqsQueuesManager.Domain;
using AmazonSqsQueuesManager.Web.Models;
using Newtonsoft.Json;

namespace AmazonSqsQueuesManager.Web.Controllers
{
	[System.Web.Http.RoutePrefix("api/queue")]
	public class QueueApiController : ApiController
	{
		private readonly AwsClient _awsClient;
		private readonly ITraceSqsRecordsService _traceSqsRecordsService;

		public QueueApiController(ITraceSqsRecordsService traceSqsRecordsService)
		{
			_awsClient = AwsClient.Instance;
			_traceSqsRecordsService = traceSqsRecordsService;
		}

		[System.Web.Http.Route("count")]
		[System.Web.Http.HttpGet]
		public int GetCount()
		{
			var numberMessagesInQueue = 0;

			try
			{
				numberMessagesInQueue = _awsClient.Count();
				_traceSqsRecordsService.Add(_awsClient.QueueName, ActionType.Count);
			}
			catch (Exception)
			{
				throw new HttpResponseException(HttpStatusCode.InternalServerError);
			}

			return numberMessagesInQueue;
		}

		[System.Web.Http.Route("dequeue")]
		[System.Web.Http.HttpGet]
		public string GetTopItem()
		{
			string messageFromQueue;

			try
			{
				messageFromQueue = _awsClient.Dequeue();

				_traceSqsRecordsService.Add(_awsClient.QueueName, ActionType.Dequeue);
			}
			catch (Exception)
			{
				var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
				{
					Content = new StringContent(string.Format("Internal server error.")),
					ReasonPhrase = "Internal server error."
				};
				throw new HttpResponseException(resp);
			}

			return messageFromQueue;
		}

		[System.Web.Http.Route("enqueue")]
		[System.Web.Http.HttpPost]
		public HttpResponseMessage PostItem(Message message)
		{
			if (message == null || message.Body==null)
			{
				var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
				{
					Content = new StringContent(string.Format("Message is absent.")),
					ReasonPhrase = "Message is absent."
				};
				throw new HttpResponseException(resp);
			}

			try
			{
				_awsClient.Enqueue(message.Body);
				_traceSqsRecordsService.Add(_awsClient.QueueName, ActionType.Enqueue);
			}
			catch (Exception)
			{
				throw new HttpResponseException(HttpStatusCode.InternalServerError);
			}
			
			return Request.CreateResponse(HttpStatusCode.OK);
		}

		[System.Web.Http.Route("purge")]
		[System.Web.Http.HttpGet]
		public HttpResponseMessage Delete()
		{
			try
			{
				_awsClient.Clear();
				_traceSqsRecordsService.Add(_awsClient.QueueName, ActionType.Purge);
			}
			catch (Exception)
			{
				throw new HttpResponseException(HttpStatusCode.InternalServerError);
			}

			return Request.CreateResponse(HttpStatusCode.OK);
		}
	}
}