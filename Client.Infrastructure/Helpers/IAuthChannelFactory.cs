using System.ServiceModel;

namespace Client.Infrastructure
{
    public interface IAuthChannelFactory
    {
        string Password { get; set; }
        string Username { get; set; }

        ChannelFactory<TChannel> GetChannelFactory<TChannel>(ushort port);
    }
}