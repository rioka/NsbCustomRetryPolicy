using System;

namespace NsbCustomRetryPolicy.Exceptions
{
  public class BusinessExceptionBase : Exception
  {
    public string Code { get; set; }

    public BusinessExceptionBase(string message) : base(message)
    {}
  }
}
