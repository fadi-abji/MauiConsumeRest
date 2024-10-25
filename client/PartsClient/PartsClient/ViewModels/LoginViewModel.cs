using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Parts.Business.Services;
using PartsClient.Dto.Data;


namespace PartsClient.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {

        public IAuthService authService { get; set; }


        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private string _errorMessage;

        [ObservableProperty]
        private bool _isBusy;

        public LoginViewModel(IAuthService authService)
        {
            this.authService = authService;
        }

        // RelayCommand to handle the login action
        [RelayCommand]
        async Task Login()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            ErrorMessage = string.Empty;

            try
            {
                var loginRequest = new LoginRequest
                {
                    Username = Username,
                    Password = Password
                };

                var IsLoggedIn = await authService.LoginAsync(loginRequest);
                if (IsLoggedIn)
                {
                    await Shell.Current.GoToAsync("partspage");
                }
                else
                {
                    ErrorMessage = "Login failed. Check your credentials.";
                }
            }
            catch (HttpRequestException)
            {
                ErrorMessage = "An error occurred. Please try again.";
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred. error message :{ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}