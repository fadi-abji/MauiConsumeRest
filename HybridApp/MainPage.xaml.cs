using HybridApp.Services;

namespace HybridApp
{
    public partial class MainPage : ContentPage
    {
        private readonly ConnectivityService _connectivityService;

        public MainPage()
        {
            InitializeComponent();
            _connectivityService = new ConnectivityService();

            if (!_connectivityService.IsConnectedToInternet())
            {
                DisplayAlert("No Internet", "Please check your internet connection.", "OK");
            }
        }

        // Be sure to dispose of the service to avoid memory leaks
        protected override void OnDisappearing()
        {
            _connectivityService.Dispose();
            base.OnDisappearing();
        }
    }
}
