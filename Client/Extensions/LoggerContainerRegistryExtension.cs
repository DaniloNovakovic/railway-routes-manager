using DryIoc;
using Prism.DryIoc;
using Prism.Ioc;

namespace Client.Extensions
{
    public static class LoggerContainerRegistryExtension
    {
        public static void RegisterLogger(this IContainerRegistry containerRegistry)
        {
            var container = containerRegistry.GetContainer();
            container.RegisterMany<Log4NetLogger>(
                Reuse.Singleton,
                ifAlreadyRegistered: IfAlreadyRegistered.Replace,
                serviceTypeCondition: t => typeof(Log4NetLogger).ImplementsServiceType(t)
            );
        }
    }
}