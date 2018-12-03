using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Messaging
{
    class EmptyListener<T> : IAsyncListener<T>
    {
        public Task NotifyAsync(T message, CancellationToken token)
        {
#if NET45
            return Task.FromResult(0);
#else
            return Task.CompletedTask;
#endif
        }
    }
}