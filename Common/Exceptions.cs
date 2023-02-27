

namespace Common
{
 
    public class BadRequestError : Exception
    {
        public BadRequestError(string msg) : base(msg)
        {
        }
    }
    public class UnauthorizedError : Exception
    {
        public UnauthorizedError(string msg) : base( msg)
        {
        }
    }
    public class ForbiddenError : Exception
    {
        public ForbiddenError(string msg) : base(msg)
        {
        }
    }
    public class NotFoundError : Exception
    {
        public NotFoundError(string msg) : base(msg)
        {
        }
    }
    public class MissingFieldError : Exception
    {
        public MissingFieldError(string fieldname) : base($"{fieldname} is required")
        {
        }
    }
    public class InternalError : Exception
    {
        public InternalError(string msg="Sever Error") : base(msg)
        {
        }
    }
}
