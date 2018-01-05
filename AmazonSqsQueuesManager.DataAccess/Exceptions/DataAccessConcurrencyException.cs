using System;
using System.Runtime.Serialization;

namespace AmazonSqsQueuesManager.DataAccess.Exceptions
{
	[Serializable]
	public class DataAccessConcurrencyException : DataAccessException, ISerializable
	{
		private const string MESSAGE = "Failed to update entity. The entity has been modified.";

		public DataAccessConcurrencyException() :
			base()
		{
		}

		public DataAccessConcurrencyException(Exception innerException) :
			this(MESSAGE, innerException)
		{

		}

		public DataAccessConcurrencyException(string message) :
			base(message)
		{
		}

		public DataAccessConcurrencyException(string message, Exception innerException) :
			base(message, innerException)
		{
		}

		protected DataAccessConcurrencyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{

		}

		public override string Message
		{
			get { return string.Format("{0};\r\n{1}", MESSAGE, this.InnerException.ToString()); }
		}
	}
}