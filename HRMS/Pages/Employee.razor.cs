using HRMS.Model;

using Microsoft.JSInterop;
using MudBlazor;
using System.Net.Http.Json;

namespace HRMS.Pages
{
    public partial class Employee
    {
        private async Task GoBack()
        {
            await JSRuntime.InvokeVoidAsync("history.back");
        }

        protected override async Task OnInitializedAsync()
        {
            await ProjectMembers();
        }

        List<HRMS.Model.EmployeeList> EmployeeListEvents = new List<HRMS.Model.EmployeeList>();

        private async Task ProjectMembers()
        {
            string apiUrl = $"api/EmployeeDetailsTable";
            using var client = HttpClientFactory.CreateClient("API");

            var response = await client.GetAsync(apiUrl).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.EmployeeList>>();
                EmployeeListEvents = json;
                StateHasChanged();
            }
            else
            {
                Snackbar.Add("Failed");
            }
        }

        EmployeeList _EmployeeList = new EmployeeList();
        public void OpenDialog(string Name)
        {

            var list = EmployeeListEvents.FirstOrDefault(x => x.Firstname1 == Name);
            if (list != null)
            {
                _EmployeeList.Firstname1 = list.Firstname1;
                _EmployeeList.Middlename1 = list.Middlename1;
                _EmployeeList.Lastname1 = list.Lastname1;
                _EmployeeList.Designation1 = list.Designation1;
                _EmployeeList.Dateofjoining = list.Dateofjoining;
            }

            OpenDialog();
        }


        private bool visible;
        private void OpenDialog() => visible = true;
        void Submit() => visible = false;
        private DialogOptions dialogOptions = new() { FullWidth = true };

        public string _noResultsMessage;
        public string _searchString;
        private Func<HRMS.Model.EmployeeList, bool> _quickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if (x.Role1.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.Firstname1.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.Middlename1.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.Lastname1.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.Email1.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.Designation1.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if ($"{x.Role1} {x.Firstname1} {x.Middlename1} {x.Lastname1} {x.Email1} {x.Designation1} ".Contains(_searchString))
                return true;

            var hasResults = EmployeeListEvents.Any(e => $"{x.Role1} {x.Firstname1} {x.Middlename1} {x.Lastname1} {x.Email1} {x.Designation1}"
       .Contains(_searchString, StringComparison.OrdinalIgnoreCase));

            if (!hasResults)
            {
                _noResultsMessage = "No Data found";
            }

            return hasResults;
        };

    }
}