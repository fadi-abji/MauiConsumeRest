using Microsoft.AspNetCore.Components;
using Parts.Business.Services;

namespace ClientParts.BlazorWebAssembly
{
    public class ClientPartsComponentBase : ComponentBase
    {
        [Inject]
        public IAuthService AuthService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IPartsService PartsService { get; set; }

    }
}
