using System;
using System.Configuration;
using System.Windows.Forms;
using GDNetworking.jsonprotocol;
using GDServices;
using GestiuneDonatii;

namespace GDClient
{
    static class StartJsonClient
    {
        private static string defaultServer = "localhost";
        private static int defaultPort = 55555;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string serverIP = ConfigurationManager.AppSettings["ServerHost"] ?? defaultServer;
            int serverPort;
            if (!int.TryParse(ConfigurationManager.AppSettings["ServerPort"], out serverPort))
            {
                Console.WriteLine("Using default port: " + defaultPort);
                serverPort = defaultPort;
            }

            Console.WriteLine("Using server IP " + serverIP);
            Console.WriteLine("Using server port " + serverPort);

            IServices server = new GDServicesJsonProxy(serverIP, serverPort);

            var ctrl = new UserController(server);
            var loginForm = new login(ctrl);

            Application.Run(loginForm);
        }
    }
}