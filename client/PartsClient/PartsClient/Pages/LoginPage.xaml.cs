using PartsClient.ViewModels;


namespace PartsClient.Pages
{
    public partial class LoginPage : ContentPage
    {

        public LoginPage(LoginViewModel loginViewModel)
        {
            InitializeComponent();
            BindingContext = loginViewModel;
        }
    }
}

