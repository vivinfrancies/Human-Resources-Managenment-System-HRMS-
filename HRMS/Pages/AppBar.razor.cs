using HRMS.Model;
using Microsoft.JSInterop;
using MudBlazor;
using System.Net.Http.Json;
using System.Text.Json;


namespace HRMS.Pages
{
    public partial class AppBar
    {
        bool _drawerOpen = false;
        AttendanceModel AttendanceModelObj = new AttendanceModel();
        void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        MudListItem selectedItem;
        object selectedValue = @MudBlazor.Color.Dark;
        private void OpenDialog()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, Position = DialogPosition.TopCenter, MaxWidth = MaxWidth.Small, FullWidth = true };
            DialogService.Show<ChangePassword>("", options);
        }
        protected override async Task OnInitializedAsync()
        {
            var userJson = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "user");
            User = JsonSerializer.Deserialize<LoginDetails>(userJson);
        }
        public async Task Clear(int EmpId)
        {
            try
            {
                AttendanceModelObj.EmpId = User.Id;
                string apiUrlSubmit = $"api/Attendances/PunchOut/{User.Id}";
                using var clientSubmit = HttpClientFactory.CreateClient("API");
                var responseSubmit = await clientSubmit.PutAsJsonAsync(apiUrlSubmit, AttendanceModelObj).ConfigureAwait(false);
                if (responseSubmit.IsSuccessStatusCode)
                {
                    Console.WriteLine("PunchOut Successfully");
                }
                else
                {
//                    Snackbar.Add("PunchOut Failed");
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Exception: {ex.Message}");
            }
            //this will clear the local storage 
            await LocalStore.ClearAsync();
            NavigationManager.NavigateTo("/");
        }
    }
}