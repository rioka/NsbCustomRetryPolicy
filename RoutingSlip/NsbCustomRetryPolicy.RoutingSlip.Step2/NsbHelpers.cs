﻿using NServiceBus;

namespace NsbCustomRetryPolicy.RoutingSlip.Step2
{
  static class MsbHelper
  {

    internal static int NumberOfRetries(this TransportMessage transportMessage)
    {
      string value;
      if (transportMessage.Headers.TryGetValue(Headers.Retries, out value))
      {
        return int.Parse(value);
      }
      return 0;
    }

    internal static string ExceptionType(this TransportMessage transportMessage)
    {
      return transportMessage.Headers["NServiceBus.ExceptionInfo.ExceptionType"];
    }

  }
}
