using System;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;

namespace Server
{
    public class AuthServiceHostFactory : IAuthServiceHostFactory
    {
        private readonly UserNamePasswordValidator _validator;

        public AuthServiceHostFactory(UserNamePasswordValidator validator)
        {
            _validator = validator;
        }

        public ServiceHost GetServiceHost<TContractType>(ushort port, object service)
        {
            var contractType = typeof(TContractType);
            return GetServiceHost(port, service, contractType);
        }

        public ServiceHost GetServiceHost(ushort port, object service, Type contractType)
        {
            string address = $"net.tcp://localhost:{port}/{contractType.Name}";
            var binding = GetBinding();

            var host = new ServiceHost(service);
            host.AddServiceEndpoint(contractType, binding, address);

            host.Credentials.ServiceCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, "localhost");
            host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;

            host.Credentials.UserNameAuthentication.UserNamePasswordValidationMode = UserNamePasswordValidationMode.Custom;
            host.Credentials.UserNameAuthentication.CustomUserNamePasswordValidator = _validator;

            return host;
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