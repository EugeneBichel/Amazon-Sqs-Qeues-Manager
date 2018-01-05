using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace AmazonSqsQueuesManager.DataAccess.Exceptions
{
	[Serializable]
	public class DataAccessException : Exception, ISerializable
	{
		private const string MESSAGE = "Data access failed for one or more entities";

		public DataAccessException() :
			base()
		{
		}

		public DataAccessException(string message)
			: base(message)
		{

		}

		public DataAccessException(string message, Exception innerException)
			: base(message, innerException)
		{

		}

		//Constructor for deserialization
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		protected DataAccessException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{

		}

		//Method for serialization
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		public override string Message
		{
			get { return string.Format("{0};\r\n{1}", MESSAGE, this.InnerException.ToString()); }
		}
	}
}