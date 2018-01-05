using System;
using System.Runtime.Serialization;

namespace AmazonSqsQueuesManager.DataAccess.Exceptions
{
	[Serializable]
	public class DataAccessValidationException : DataAccessException, ISerializable
	{
		private const string MESSAGE = "Validation failed for the entities.";

		public DataAccessValidationException() :
			base()
		{

		}

		public DataAccessValidationException(Exception innerException) :
			this(MESSAGE, innerException)
		{

		}

		public DataAccessValidationException(string message) :
			base(message)
		{

		}

		public DataAccessValidationException(string message, Exception innerException) :
			base(message, innerException)
		{
		}

		protected DataAccessValidationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{

		}

		public override string Message
		{
			get { return string.Format("{0};\r\n{1}", MESSAGE, this.InnerException.ToString()); }
		}
	}
}