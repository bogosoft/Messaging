using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Messaging
{
    /// <summary>
    /// An implementation of the <see cref="IAsyncListener{T}"/> contract that treats
    /// a collection of listeners as if they were a single listener.
    /// </summary>
    /// <typeparam name="T">The type of the message capable of being listened for and handled.</typeparam>
    public sealed class CompositeAsyncListener<T> : IAsyncListener<T>
    {
        readonly IAsyncListener<T>[] listeners;

        /// <summary>
        /// Create a new instance of the <see cref="CompositeAsyncListener{T}"/> class.
        /// </summary>
        /// <param name="listeners">A collection of listeners with which to create a composite.</param>
        public CompositeAsyncListener(IEnumerable<IAsyncListener<T>> listeners)
        {
            this.listeners = listeners.ToArray();
        }

        /// <summary>
        /// Create a new instance of the <see cref="CompositeAsyncListener{T}"/> class.
        /// </summary>
        /// <param name="listeners">A collection of listeners with which to create a composite.</param>
        public CompositeAsyncListener(params IAsyncListener<T>[] listeners)
        {
            this.listeners = listeners;
        }

        /// <summary>
        /// Notify the current listener of a new message to be handled.
        /// </summary>
        /// <param name="message">A message to be handled.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>A task representing a possibly asynchronous operation.</returns>
        public Task NotifyAsync(T message, CancellationToken token)
        {
            Task WithCancellationToken(IAsyncListener<T> listener)
            {
                return listener.NotifyAsync(message, token);
            }

            return Task.WhenAll(listeners.Select(WithCancellationToken));
        }
    }
}