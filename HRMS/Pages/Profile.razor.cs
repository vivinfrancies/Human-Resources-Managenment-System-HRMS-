using Microsoft.JSInterop;
using MudBlazor;
using System.Net.Http.Json;
using System.Net.Http;
using HRMS.Model;
using System.Text.Json;

namespace HRMS.Pages
{
    public partial class Profile
    {

      
        List<HRMS.Model.EmployeeList> ProfileList = new List<HRMS.Model.EmployeeList>();
        string[] Familytable = { "Name", "Relationship", "Date of Birth", "Phone" };
        string[] Familyvalue = { @"Mathan Brother 21-12-2001 7402109346", };

      
       


        public async Task ProfileDetails()
        {
            try
            {
                string apiUrl = $"api/EmployeeDetailsTable/Employeeoveralldetails/{User.UserName}";
                using var client = HttpClientFactory.CreateClient("API");
                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.EmployeeList>>();
                    ProfileList = json;
                    StateHasChanged();
                }
                else
                {
                    Console.WriteLine("Failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        protected override async Task OnInitializedAsync()
        {
            var userJson = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "user");
            User = JsonSerializer.Deserialize<LoginDetails>(userJson);
            await ProfileDetails();
        }
            private void OpenDialog()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, Position = DialogPosition.TopCenter, MaxWidth = MaxWidth.Medium, FullWidth = true };
         
        }
        private async Task GoBack()
        {
            await JSRuntime.InvokeVoidAsync("history.back");
        }

    }

}