using Lab_5_App.Services;
using Microsoft.AspNetCore.Components;

namespace Lab_5_App.Components.Pages
{
    public partial class Home
    {
        [Inject]
        ILibraryService? LibServ { get; set; }
        protected override async Task OnInitializedAsync()
        {
            
        }
    }
}
