using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using Web.Services;

namespace Web.Components.Layout
{
    public partial class Appbar
    {
        private bool _searchDialogOpen;
        private bool _searchDialogAutocompleteOpen;
        private int _searchDialogReturnedItemsCount;
        public bool DisplaySearchBar { get; set; } = true;
        private DialogOptions _dialogOptions = new() { Position = DialogPosition.TopCenter, NoHeader = true };
        [Parameter] public EventCallback<MouseEventArgs> DrawerToggleCallback { get; set; }
        [Inject] private LayoutService LayoutService { get; set; }
        [Inject] NavigationManager navManager { get; set; }
        [Inject] AuthenticationStateProvider authStateProvicder { get; set; }
        [Parameter]public string avaSource { get; set; }

        private async Task Logout()
        {
            // var customAuthStateProvider = (CustomAuthenticationStateProvider)authStateProvicder;
            // await customAuthStateProvider.UpdateAuthenticationState(null);
            navManager.NavigateTo("/");

        }
        public bool IsSearchDialogOpen
        {
            get => _searchDialogOpen;
            set
            {
                _searchDialogAutocompleteOpen = default;
                _searchDialogReturnedItemsCount = default;
                _searchDialogOpen = value;
            }
        }
        private void OpenSearchDialog() => IsSearchDialogOpen = true;
    }
}
