
using System;

namespace NsbCustomRetryPolicy.Messages.Events
{
  public class OrderCreated
  {
    public Guid Id { get; set; }
    public string Notes { get; set; }
  }
}
