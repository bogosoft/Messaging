using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Messaging
{
    /// <summary>
    /// A set of static members for working with <see cref="IAsyncListener{T}"/> types.
    /// </summary>
    public static class AsyncListener
    {
        /// <summary>
        /// Synchronously notify the current listener of a new message to be handled.
        /// </summary>
        /// <typeparam name="T">The type of the message capable of being listened for and handled.</typeparam>
        /// <param name="listener">The current listener.</param>
        /// <param name="message">A message to be handled.</param>
        public static void Notify<T>(this IAsyncListener<T> listener, T message)
        {
            Task.Run(async () => await listener.NotifyAsync(message).ConfigureAwait(false)).Wait();
        }

        /// <summary>
        /// Notify the current listener of a new message to be handled.
        /// </summary>
        /// <typeparam name="T">The type of the message capable of being listened for and handled.</typeparam>
        /// <param name="listener">The current listener.</param>
        /// <param name="message">A message to be handled.</param>
        /// <returns>A task representing a possibly asynchronous operation.</returns>
        public static Task NotifyAsync<T>(this IAsyncListener<T> listener, T message)
        {
            return listener.NotifyAsync(message, CancellationToken.None);
        }
    }

    /// <summary>
    /// A set of static members for working with <see cref="IAsyncListener{T}"/> types.
    /// </summary>
    public static class AsyncListener<T>
    {
        /// <summary>
        /// Get an empty listener, i.e. a listener that takes no action other than to return a completed task.
        /// </summary>
        public static IAsyncListener<T> Empty => new EmptyListener<T>();
    }
}