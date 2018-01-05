using System.Collections.Generic;
using System.Web.Http;
using AmazonSqsQueuesManager.Business.Models;
using AmazonSqsQueuesManager.Business.Services;

namespace AmazonSqsQueuesManager.Web.Controllers
{
	[RoutePrefix("api/trace")]
	public class TraceOperationsApiController : ApiController
	{
		private readonly ITraceSqsRecordsService _traceSqsRecordsService;

		public TraceOperationsApiController(ITraceSqsRecordsService traceSqsRecordsService)
		{
			_traceSqsRecordsService = traceSqsRecordsService;
		}

		[HttpGet]
		[Route("last")]
		public TracingRecord GetLastAdded()
		{
			return _traceSqsRecordsService.GetLastAddedRecord();
		}

		[HttpGet]
		[Route("all")]
		public IEnumerable<TracingRecord> GetAllRecords()
		{
			return _traceSqsRecordsService.GetAllRecords();
		}

		[HttpGet]
		[Route("clear")]
		public void DeleteAllRecords()
		{
			_traceSqsRecordsService.DeleteAllRecords();
		}
	}
}