using HRMS.Model;
using Microsoft.JSInterop;
using MudBlazor;
using System.Net.Http.Json;
using System.Net.Http;
using ApexCharts;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace HRMS.Pages
{
    public partial class Payroll
    {
        #region Properties
        List<HRMS.Model.PayslipDetails> individualpayrollhistory = new List<HRMS.Model.PayslipDetails>();
        [Parameter]
        public string EmployeeCode { get; set; }
        private string _searchStrings;
        public string _noResultsMessages;
        #endregion
        protected override async Task OnInitializedAsync()
        {
            var userJson = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "user");
            User = JsonSerializer.Deserialize<LoginDetails>(userJson);
            await IndividualPayroll();
        }

        #region IndividualPayroll
        private async Task IndividualPayroll()
        {
            try
            {

                string apiUrl = $"api/Payroll/PayslipDetails/{User.UserName}";
                using var client = HttpClientFactory.CreateClient("API");
                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.PayslipDetails>>();
                    individualpayrollhistory = json;
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching data: {ex.Message}");
            }
        }
        #endregion

        #region Filter
        private List<string> _eventss = new();
        private Func<HRMS.Model.PayslipDetails, bool> _quickFilters => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchStrings))
                return true;

            if ($"{x.CreatedAt} ".Contains(_searchStrings, StringComparison.OrdinalIgnoreCase))
                return true;

            var hasResults = individualpayrollhistory.Any(e => $"{x.CreatedAt}"
            .Contains(_searchStrings, StringComparison.OrdinalIgnoreCase));
            if (!hasResults)
            {
                _noResultsMessages = "No Data found";
            }

            return hasResults;
        };

        #endregion
        void generateslip(int ID)
        {
            NavigationManager.NavigateTo($"/generateslip/{ID}");
        }
        private async Task GoBack()
        {
            await JSRuntime.InvokeVoidAsync("history.back");
        }

    }
}