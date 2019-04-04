using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelpScout.Tests
{
    public static class Retry
    {
        public static async Task<T> DoAsync<T>(Func<Task<T>> action, int maxAttemptCount = 3)
        {
            var retryInterval = TimeSpan.FromSeconds(3);
            var exceptions = new List<Exception>();

            for (var attempted = 0; attempted < maxAttemptCount; attempted++)
                try
                {
                    if (attempted > 0) await Task.Delay(retryInterval);

                    return await action();
                }
                catch (HelpScoutException) //Throw if helpscout exception
                {
                    throw;
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }

            throw new AggregateException(exceptions);
        }
    }
}