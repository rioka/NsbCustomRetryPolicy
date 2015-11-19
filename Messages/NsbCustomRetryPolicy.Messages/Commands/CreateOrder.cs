using System;

namespace NsbCustomRetryPolicy.Messages.Commands
{
  public class CreateOrder
  {
    public Guid Id { get; set; }
    public string Customer { get; set; }
    public DateTimeOffset Date { get; set; }
  }
}
