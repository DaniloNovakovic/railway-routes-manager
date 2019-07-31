using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;

namespace Client.Infrastructure
{
    public class AuthChannelFactory : IAuthChannelFactory
    {
        public string Password { get; set; }
        public string Username { get; set; }

        public ChannelFactory<TChannel> GetChannelFactory<TChannel>(ushort port)
        {
            var binding = GetBinding();

            var channelType = typeof(TChannel);
            string address = $"net.tcp://localhost:{port}/{channelType.Name}";

            var factory = new ChannelFactory<TChannel>(binding, address);
            factory.Credentials.UserName.UserName = Username;
            factory.Credentials.UserName.Password = Password;
            factory.Credentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, "localhost");
            factory.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;

            return factory;
        }

        private static Binding GetBinding()
        {
            var binding = new NetTcpBinding();
            binding.Security.Mode = SecurityMode.Message;
            binding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;
            return binding;
        }
    }
}