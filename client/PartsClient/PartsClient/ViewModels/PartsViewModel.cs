using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PartsClient.Data;
using PartsClient.Service;
using System.Collections.ObjectModel;

namespace PartsClient.ViewModels;

public partial class PartsViewModel : ObservableObject
{
    private readonly IPartsService _partsService;

    [ObservableProperty]
    ObservableCollection<Part> _parts;


    [ObservableProperty]
    bool _isRefreshing = false;


    [ObservableProperty]
    bool _isBusy = false;


    [ObservableProperty]
    Part _selectedPart;

    public PartsViewModel(IPartsService partsService)
    {

        _partsService = partsService;

        _parts = new ObservableCollection<Part>();

        WeakReferenceMessenger.Default.Register<RefreshMessage>(this, async (r, m) =>
        {
            await LoadData();
        });

        Task.Run(LoadData);
    }

    [RelayCommand]
    async Task PartSelected()
    {
        if (SelectedPart == null)
            return;

        var navigationParameter = new Dictionary<string, object>()
        {
            { "part", SelectedPart }
        };

        await Shell.Current.GoToAsync("addpart", navigationParameter);

        MainThread.BeginInvokeOnMainThread(() => SelectedPart = null);
    }

    [RelayCommand]
    async Task LoadData()
    {
        if (IsBusy)
            return;

        try
        {
            IsRefreshing = true;
            IsBusy = true;

            var partsCollection = await _partsService.GetAll();

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Parts.Clear();

                foreach (Part part in partsCollection)
                {
                    Parts.Add(part);
                }
            });
        }
        finally
        {
            IsRefreshing = false;
            IsBusy = false;
        }
    }

    [RelayCommand]
    async Task AddNewPart()
    {
        await Shell.Current.GoToAsync("addpart");
    }

}
