using System;
using NsbCustomRetryPolicy.Messages.Commands;
using NsbCustomRetryPolicy.Messages.Events;
using NServiceBus;

namespace NsbCustomRetryPolicy.RoutingSlip.Step3
{
  public class MessageHandler : IHandleMessages<CreateOrder>, IHandleMessages<OrderCreated>
  {
    private readonly IBus _bus;

    public MessageHandler(IBus bus)
    {
      _bus = bus;
    }

    public void Handle(CreateOrder message)
    {
      Console.WriteLine("Creating order '{0}' for customer '{1}', step3", message.Id, message.Customer);
    }

    public void Handle(OrderCreated message)
    {
      throw new NotImplementedException();
    }
  }
}
