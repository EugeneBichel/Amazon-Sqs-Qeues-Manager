using System;
using System.Runtime.Serialization;

namespace AmazonSqsQueuesManager.DataAccess.Exceptions
{
	[Serializable]
	public class DataAccessUpdateException : DataAccessException, ISerializable
	{
		private const string MESSAGE = "Database updating failed.";

		public DataAccessUpdateException() :
			base()
		{
		}

		public DataAccessUpdateException(string message) :
			base(message)
		{
		}

		public DataAccessUpdateException(string message, Exception innerException) :
			base(message, innerException)
		{
		}

		protected DataAccessUpdateException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{

		}

		public override string Message
		{
			get { return string.Format("{0};\r\n{1}", MESSAGE, this.InnerException.ToString()); }
		}
	}
}