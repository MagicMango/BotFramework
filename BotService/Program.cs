using System.ServiceProcess;

namespace BotService
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new MyBotService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
