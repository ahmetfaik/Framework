using System;
using System.Runtime.Serialization;

namespace Framework.Core.Common
{
    public class SiteException : Exception
    {

        public SiteException() { }

        public SiteException(string message) : base(message) { }

        public SiteException(string messageFormat, params object[] args) : base(string.Format(messageFormat, args)) { }

        public SiteException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public SiteException(string message, Exception innerException) : base(message, innerException) { }

    }
}
