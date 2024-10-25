using PartsClient.ViewModels;

namespace PartsClient.Pages;

public partial class PartsPage : ContentPage
{
    public PartsPage(PartsViewModel partsViewModel)
    {
        InitializeComponent();
        BindingContext = partsViewModel;
    }
}