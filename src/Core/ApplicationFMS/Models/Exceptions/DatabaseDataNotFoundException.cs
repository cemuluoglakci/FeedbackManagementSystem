using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace ApplicationFMS.Models.Exceptions
{
    public class DatabaseDataNotFoundException : Exception
    {
        public DatabaseDataNotFoundException() : base() { }

        public DatabaseDataNotFoundException(string message) : base(message) { }

        public DatabaseDataNotFoundException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args)) { }

        public DatabaseDataNotFoundException(string message, Exception inner)
            : base(message, inner) { }

        public DatabaseDataNotFoundException(string message, Exception inner, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args), inner) { }

        protected DatabaseDataNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
