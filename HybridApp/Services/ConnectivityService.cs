namespace HybridApp.Services
{
    public class ConnectivityService
    {
        public ConnectivityService()
        {
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }
        ~ConnectivityService()
        {
            Dispose();
        }

        public bool IsConnectedToInternet()
        {
            return Connectivity.Current.NetworkAccess == NetworkAccess.Internet;
        }

        public bool IsConnectedLocally()
        {
            return Connectivity.Current.NetworkAccess == NetworkAccess.Local;
        }

        public bool IsConnectedViaWifi()
        {
            return Connectivity.Current.ConnectionProfiles.Contains(ConnectionProfile.WiFi);
        }

        // Event handler for connectivity changes
        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            var networkAccess = e.NetworkAccess;

            if (networkAccess == NetworkAccess.Internet)
            {
                Console.WriteLine("Internet is available.");
            }
            else if (networkAccess == NetworkAccess.None)
            {
                Console.WriteLine("No internet access.");
            }
            else
            {
                Console.WriteLine($"Network access changed to: {networkAccess}");
            }
            // Log each active connection
            Console.Write("Connections active: ");

            foreach (var item in e.ConnectionProfiles)
            {
                switch (item)
                {
                    case ConnectionProfile.Bluetooth:
                        Console.Write("Bluetooth");
                        break;
                    case ConnectionProfile.Cellular:
                        Console.Write("Cell");
                        break;
                    case ConnectionProfile.Ethernet:
                        Console.Write("Ethernet");
                        break;
                    case ConnectionProfile.WiFi:
                        Console.Write("WiFi");
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine();
        }

        public void Dispose()
        {
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }

    }
}
