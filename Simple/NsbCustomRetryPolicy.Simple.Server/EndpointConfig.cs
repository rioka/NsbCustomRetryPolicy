
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NsbCustomRetryPolicy.Exceptions;

namespace NsbCustomRetryPolicy.Simple.Server
{
  using NServiceBus;

  public class EndpointConfig : IConfigureThisEndpoint
  {
    /// <summary>
    /// Business logic exception types
    /// </summary>
    private static readonly IList<string> BusinessLogicExceptionTypes = new List<string>(
      Assembly.GetAssembly(typeof(BusinessExceptionBase))
              .GetTypes()
              .Where(t => t != typeof(BusinessExceptionBase)
                          && typeof(BusinessExceptionBase).IsAssignableFrom(t)).Select(t => t.FullName));

    public void Customize(BusConfiguration configuration)
    {
      configuration.EndpointName(GetType().Namespace);
      configuration.UseTransport<MsmqTransport>();
      configuration.UsePersistence<InMemoryPersistence>();
      configuration.EnableInstallers();
      configuration.UseSerialization<JsonSerializer>();

      configuration.SecondLevelRetries().CustomRetryPolicy(CustomRetryPolicy);

      configuration.Conventions()
                   .DefiningMessagesAs(t => t.Namespace != null && t.Namespace.EndsWith(".Messages"))
                   .DefiningCommandsAs(t => t.Namespace != null && t.Namespace.EndsWith(".Commands"))
                   .DefiningEventsAs(t => t.Namespace != null && t.Namespace.EndsWith(".Events"));
    }

    private TimeSpan CustomRetryPolicy(TransportMessage message)
    {
      // Stop second level retries in case of a business exception
      if (BusinessLogicExceptionTypes.Contains(message.ExceptionType()))
      {
        Console.WriteLine("Dropping Slr for message '{0}'", message.Id);
        return TimeSpan.MinValue;
      }

      return TimeSpan.FromSeconds(10);
    }
  }
}
