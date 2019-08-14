using Prism.Events;

namespace Client.Core
{
    /// <summary>
    /// `int` from PubSubEvent&lt;int&gt; represents `id` of updated Route
    /// </summary>
    public class RouteUpdatedEvent : PubSubEvent<int>
    {
    }
}