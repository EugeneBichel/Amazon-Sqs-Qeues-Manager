using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;
using AmazonSqsQueuesManager.Business.Services;
using AmazonSqsQueuesManager.DAL.EF;
using AmazonSqsQueuesManager.DataAccess;
using Microsoft.Practices.Unity;

namespace AmazonSqsQueuesManager.Web.Bootstrap
{
	public static class IoCContainer
	{
		public static IUnityContainer Container { get; private set; }

		public static IUnityContainer Initialise()
		{
			Container = BuildUnityContainer();

			DependencyResolver.SetResolver(new UnityResolver(Container));
			GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(Container);

			return Container;
		}

		private static IUnityContainer BuildUnityContainer()
		{
			var container = new UnityContainer();

			RegisterTypes(container);

			return container;
		}

		public static void RegisterTypes(IUnityContainer container)
		{
			container.RegisterType<ITraceRecordsRepository, TraceRecordsRepository>();
			container.RegisterType<ITraceSqsRecordsService, TraceSqsRecordsService>();
		}
	}

	public class UnityResolver : IDependencyResolver
	{
		protected IUnityContainer container;

		public UnityResolver(IUnityContainer container)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			this.container = container;
		}

		public object GetService(Type serviceType)
		{
			try
			{
				return container.Resolve(serviceType);
			}
			catch (ResolutionFailedException)
			{
				return null;
			}
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			try
			{
				return container.ResolveAll(serviceType);
			}
			catch (ResolutionFailedException)
			{
				return new List<object>();
			}
		}

		public void Dispose()
		{
			container.Dispose();
		}
	}
}
