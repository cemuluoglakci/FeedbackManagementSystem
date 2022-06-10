using System;

namespace ApplicationFMS.Models.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() : base("Unauthorized!")
        {
        }
    }
}
