using Prism.Events;

namespace Client.Core
{
    /// <summary>
    /// `string` from PubSubEvent&lt;string&gt; represents `username` of added user
    /// </summary>
    public class UserAddedEvent : PubSubEvent<string>
    {
    }
}