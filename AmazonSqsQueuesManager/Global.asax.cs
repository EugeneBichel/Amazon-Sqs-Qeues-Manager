using AmazonSqsQueuesManager.Web.Bootstrap;

namespace AmazonSqsQueuesManager.Web
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			new Bootstrapper().Bootstrap();
		}
	}
}