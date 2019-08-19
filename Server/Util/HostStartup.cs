using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Core;

namespace Server
{
    public static class HostStartup
    {
        public static void CloseHosts(IEnumerable<System.ServiceModel.ICommunicationObject> hosts, ILogger logger)
        {
            logger.Debug("Closing hosts...");

            var tasks = new List<Task>();

            foreach (var host in hosts)
            {
                tasks.Add(Task.Run(() =>
                {
                    try
                    {
                        host.Close();
                    }
                    catch (Exception ex)
                    {
                        logger.Exception(ex.Message);
                    }
                }));
            }

            Task.WhenAll(tasks).GetAwaiter().GetResult();

            logger.Info("All hosts are closed!");
        }

        public static void StartHosts(IEnumerable<System.ServiceModel.ICommunicationObject> hosts, ILogger logger)
        {
            logger.Debug("Starting hosts...");

            var tasks = new List<Task>();

            foreach (var host in hosts)
            {
                tasks.Add(Task.Run(() =>
                {
                    try
                    {
                        host.Open();
                    }
                    catch (Exception ex)
                    {
                        logger.Exception(ex.Message);
                    }
                }));
            }

            Task.WhenAll(tasks).GetAwaiter().GetResult();

            logger.Info("All hosts are opened!");
        }
    }
}