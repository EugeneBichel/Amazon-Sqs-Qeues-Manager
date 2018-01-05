using System;
using AutoMapper;
using Resource = AmazonSqsQueuesManager.Business.Models;
using Model = AmazonSqsQueuesManager.Domain;

namespace AmazonSqsQueuesManager.Business.Mapping
{
	public static class TraceRecordMapper
	{
		static TraceRecordMapper()
		{
			try
			{
				InitializeResourceMapping();
				InitializeModelMapping();
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Unable to initialize Club model<->business mapping", ex);
			}
		}

		public static Resource.TracingRecord MapModelToResource(Model.TracingRecord model)
		{
			return Mapper.Map<Model.TracingRecord, Resource.TracingRecord>(model);
		}

		public static Model.TracingRecord MapResourceToModel(Resource.TracingRecord resource)
		{
			return Mapper.Map<Resource.TracingRecord, Model.TracingRecord>(resource);
		}

		/// <summary>
		/// Creates model->resource mappers
		/// </summary>
		private static void InitializeResourceMapping()
		{
			Mapper.CreateMap<Model.TracingRecord, Resource.TracingRecord>()
				.ForMember(dest => dest.QueueName, opt => opt.MapFrom(src => src.QueueName))
				.ForMember(dest => dest.TimeStamp, opt => opt.ResolveUsing<TracingRecordTimestampResolver>())
				.ForMember(dest => dest.ActionType, opt => opt.ResolveUsing<TracingRecordActionTypeResolver>());
		}

		/// <summary>
		/// Creates resource->model mappers
		/// </summary>
		private static void InitializeModelMapping()
		{
			Mapper.CreateMap<Resource.TracingRecord, Model.TracingRecord>()
				.ForMember(dest => dest.QueueName, opt => opt.MapFrom(src => src.QueueName))
				.ForMember(dest => dest.TimeStamp, opt => opt.ResolveUsing<ModelTracingRecordTimestampResolver>())
				.ForMember(dest => dest.ActionType, opt => opt.ResolveUsing<ModelTracingRecordActionTypeResolver>());
		}

		//To resource model
		public class TracingRecordTimestampResolver :
				ValueResolver<Model.TracingRecord, string>
		{
			protected override string ResolveCore(Model.TracingRecord tracingRecord)
			{
				return tracingRecord.TimeStamp.ToString("G");
			}
		}

		//To resource model
		public class TracingRecordActionTypeResolver :
				ValueResolver<Model.TracingRecord, string>
		{
			protected override string ResolveCore(Model.TracingRecord tracingRecord)
			{
				return Enum.GetName(tracingRecord.ActionType.GetType(), tracingRecord.ActionType);
			}
		}

		//To domain model
		public class ModelTracingRecordTimestampResolver :
				ValueResolver<Resource.TracingRecord, DateTime>
		{
			protected override DateTime ResolveCore(Resource.TracingRecord tracingRecord)
			{
				return DateTime.Parse(tracingRecord.TimeStamp);
			}
		}

		//To domain model
		public class ModelTracingRecordActionTypeResolver :
				ValueResolver<Resource.TracingRecord, Model.ActionType>
		{
			protected override Model.ActionType ResolveCore(Resource.TracingRecord tracingRecord)
			{
				return (Model.ActionType)Enum.Parse(typeof(Model.ActionType), tracingRecord.ActionType, true);
			}
		}
	}
}