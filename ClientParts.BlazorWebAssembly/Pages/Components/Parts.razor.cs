using PartsClient.Dto.Data;

namespace ClientParts.BlazorWebAssembly.Pages.Components
{
    public partial class Parts : ClientPartsComponentBase
    {
        private List<Part> parts;

        protected override async Task OnInitializedAsync()
        {
            await LoadPartsAsync();
        }

        private async Task LoadPartsAsync()
        {
            // Simulated data fetching for demonstration
            await Task.Delay(500); // Simulate delay

            parts = new List<Part>
        {
            new Part { PartID = "P001", PartName = "Widget A", PartType = "Type A", PartAvailableDate = DateTime.Now, Suppliers = new List<string> { "Supplier1" } },
            new Part { PartID = "P002", PartName = "Widget B", PartType = "Type B", PartAvailableDate = DateTime.Now.AddDays(1), Suppliers = new List<string> { "Supplier2", "Supplier3" } },
            new Part { PartID = "P003", PartName = "Widget C", PartType = "Type C", PartAvailableDate = DateTime.Now.AddDays(2), Suppliers = new List<string> { "Supplier4" } }
            // Add more sample parts as needed
        };
        }
    }
}
