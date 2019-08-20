using Prism.Events;

namespace Client.Core
{
    /// <summary>
    /// `string` from PubSubEvent&lt;string&gt; represents message to display in snackbar
    /// </summary>
    public class SnackbarMessageEvent : PubSubEvent<string>
    {
    }
}