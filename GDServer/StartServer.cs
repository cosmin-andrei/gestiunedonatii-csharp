using GDNetworking.utils;
using GDServices;
using GestiuneDonatii.Repository;
using GestiuneDonatii.service;

public class StartJsonServer
    {
        private static int defaultPort = 55555;

        static void Main(string[] args)
        {
            var serverProps = new Dictionary<string, string>();
            try
            {
                var propsText = File.ReadAllLines("server.properties");
                foreach (var line in propsText)
                {
                    var parts = line.Split('=');
                    if (parts.Length == 2)
                        serverProps[parts[0]] = parts[1];
                }
                Console.WriteLine("Server properties set. ");
                foreach (var prop in serverProps)
                    Console.WriteLine(prop.Key + ": " + prop.Value);
            }
            catch (IOException e)
            {
                Console.WriteLine("Cannot find properties " + e.Message);
                return;
            }

            IDonatorRepo donatorRepo = new DonatorRepo(serverProps);
            IDonatieRepo donatieRepo = new DonatieRepo(serverProps);
            ICauzaRepo cauzaRepo = new CauzaRepo(serverProps);
            IVoluntarRepo voluntarRepo = new VoluntarRepo(serverProps);
            IServices service = new GDServicesImpl(donatorRepo, donatieRepo, cauzaRepo, voluntarRepo);
            int serverPort = defaultPort;
            try
            {
                serverPort = int.Parse(serverProps["server.port"]);
            }
            catch (FormatException nef)
            {
                Console.WriteLine("Wrong Port Number" + nef.Message);
                Console.WriteLine("Using default port " + defaultPort);
            }
            Console.WriteLine("Starting server on port: " + serverPort);
            AbstractServer server = new GDJsonConcurrentServer(serverPort, service);
            try
            {
                server.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error starting the server: " + e.Message);
            }
        }
    }