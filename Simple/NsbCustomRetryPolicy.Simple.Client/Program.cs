using System;
using NsbCustomRetryPolicy.Messages.Commands;

namespace NsbCustomRetryPolicy.Simple.Client
{
  class Program
  {
    static void Main(string[] args)
    {
      ServiceBus.Init();
      var bus = ServiceBus.Bus;

      while (true)
      {
        Console.WriteLine("Enter 'W' to start a Wallet to Wallet transfer");
        Console.WriteLine("Enter 'B' to start a Wallet to Bank transfer");
        Console.WriteLine("Press any other key to exit");

        var input = (Console.ReadLine() ?? "").ToUpper();

        if (string.IsNullOrWhiteSpace(input))
        {
          break;
        }

        var id = Guid.NewGuid();
        bus.Send("NsbCustomRetryPolicy.Simple.Server", new CreateOrder() {
          Customer = "John Doe",
          Date = DateTimeOffset.Now,
          Id = id
        });

        Console.WriteLine("request '{0}' sent", id);
      }

      bus.Dispose();
    }
  }
}
