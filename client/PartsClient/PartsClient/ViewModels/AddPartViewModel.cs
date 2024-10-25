using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PartsClient.Data;
using PartsClient.Service;

namespace PartsClient.ViewModels;

public partial class AddPartViewModel : ObservableObject
{

    private readonly IPartsService _partsService;

    [ObservableProperty]
    string _partID;

    [ObservableProperty]
    string _partName;

    [ObservableProperty]
    string _suppliers;

    [ObservableProperty]
    string _partType;

    public AddPartViewModel(IPartsService partsService)
    {
        _partsService = partsService;
    }

    [RelayCommand]
    async Task SaveData()
    {
        if (string.IsNullOrWhiteSpace(PartID))
            await InsertPart();
        else
            await UpdatePart();
    }

    [RelayCommand]
    async Task InsertPart()
    {
        await _partsService.Add(PartName, Suppliers, PartType);

        WeakReferenceMessenger.Default.Send(new RefreshMessage(true));

        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task UpdatePart()
    {
        Part partToSave = new()
        {
            PartID = PartID,
            PartName = PartName,
            PartType = PartType,
            Suppliers = Suppliers.Split(",").ToList()
        };

        await _partsService.Update(partToSave);

        WeakReferenceMessenger.Default.Send(new RefreshMessage(true));

        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task DeletePart()
    {
        if (string.IsNullOrWhiteSpace(PartID))
            return;

        await _partsService.Delete(PartID);

        WeakReferenceMessenger.Default.Send(new RefreshMessage(true));

        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task DoneEditing()
    {
        await Shell.Current.GoToAsync("..");
    }
}
