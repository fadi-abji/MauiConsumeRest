using PartsClient.Pages;

namespace PartsClient;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("login", typeof(LoginPage));
        Routing.RegisterRoute("addpart", typeof(AddPartPage));
        Routing.RegisterRoute("partspage", typeof(PartsPage));
    }
}

