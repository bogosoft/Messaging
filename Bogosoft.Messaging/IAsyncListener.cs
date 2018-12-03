using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Messaging
{
    /// <summary>
    /// Represents any type capable of listening for and handling messages of a specified type.
    /// </summary>
    /// <typeparam name="T">The type of the message capable of being listened for and handled.</typeparam>
    public interface IAsyncListener<in T>
    {
        /// <summary>
        /// Notify the current listener of a new message to be handled.
        /// </summary>
        /// <param name="message">A message to be handled.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>A task representing a possibly asynchronous operation.</returns>
        Task NotifyAsync(T message, CancellationToken token);
    }
}