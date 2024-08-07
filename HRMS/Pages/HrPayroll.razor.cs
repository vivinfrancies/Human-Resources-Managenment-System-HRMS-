using HRMS.Model;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Net.Http;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Text.Json;
using Microsoft.VisualBasic;


namespace HRMS.Pages
{
    public partial class HrPayroll
    {
        #region Properties
        public string _noResultsMessage;
        public string _searchString;
        private List<HRMS.Model.AllEmployeeDetails> allemployeedetails = new List<AllEmployeeDetails>();
        List<HRMS.Model.PayslipDetails> CheckPayrollDateList = new List<HRMS.Model.PayslipDetails>();
        #endregion
        protected override async Task OnInitializedAsync()
        {
            var userJson = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "user");
            User = JsonSerializer.Deserialize<LoginDetails>(userJson);

            await GettingEmployeeDetails();

        }
        #region AllEmployeeDetails
        private async Task GettingEmployeeDetails()
        {
            try
            {
                string apiUrl = $"api/Payroll/AllEmployeeDetails{User.UserName}";
                using var client = HttpClientFactory.CreateClient("API");
                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadFromJsonAsync<List<AllEmployeeDetails>>();
                    allemployeedetails = json;
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching data: {ex.Message}");
            }
        }
        #endregion

        #region Checkpayrolldate

        private async Task<List<DateTime>> CheckPayrollDate(string code)
        {
            List<DateTime> createdDates = new List<DateTime>();
            try
            {
                string apiUrl = $"api/Payroll/PayslipDetails/{code}";
                using var client = HttpClientFactory.CreateClient("API");
                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadFromJsonAsync<List<PayslipDetails>>();
                    CheckPayrollDateList = json;
                    if (CheckPayrollDateList != null && CheckPayrollDateList.Any())
                    {
                        createdDates = CheckPayrollDateList.Select(p => p.CreatedAt).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching data: {ex.Message}");
            }
            return createdDates; 
        }
        #endregion

        #region Filter
        private Func<AllEmployeeDetails, bool> _quickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if ($"{x.Designation} {x.EmployeeName} {x.ID} {x.EmployeeCode} ".Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            var hasResults = allemployeedetails.Any(e => $"{x.Designation} {x.EmployeeName} {x.ID} {x.EmployeeCode}"
       .Contains(_searchString, StringComparison.OrdinalIgnoreCase));

            if (!hasResults)
            {
                _noResultsMessage = "No Data found";
            }

            return hasResults;
        };

        #endregion

        #region Generateslip
        private async Task generateslip(string code)
        {
            var createdDates = await CheckPayrollDate(code);
            bool isAlreadyCreated = createdDates.Any(date => date.Month == DateTime.Now.Month && date.Year == DateTime.Now.Year);

            if (isAlreadyCreated)
            {
                Snackbar.Add($" Salary Updated", Severity.Info);

            }
            else
            {
                NavigationManager.NavigateTo($"/editsalary/{code}");
            }
        }
        #endregion
        private async Task GoBack()
        {
            await JSRuntime.InvokeVoidAsync("history.back");
        }
    }
}