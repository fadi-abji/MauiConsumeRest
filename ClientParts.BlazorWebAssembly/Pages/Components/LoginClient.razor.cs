using PartsClient.Dto.Data;

namespace ClientParts.BlazorWebAssembly.Pages.Components
{
    public partial class LoginClient : ClientPartsComponentBase
    {
        private LoginRequest loginModel = new LoginRequest();

        private async Task HandleValidSubmit()
        {
            // Simulate login logic here, e.g., call an API
            Console.WriteLine($"Username: {loginModel.Username}, Password: {loginModel.Password}");

            // Redirect or perform other actions after successful login
            // For example, using NavigationManager to redirect to another page
            // NavigationManager.NavigateTo("/home");
            bool SuccessLogIn = await AuthService.LoginAsync(loginModel);
            if (SuccessLogIn)
            {
                NavigationManager.NavigateTo("/parts");
            }
            else
            {
                // Show error message
            }
        }
    }
}
