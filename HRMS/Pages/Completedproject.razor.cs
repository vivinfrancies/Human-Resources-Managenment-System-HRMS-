using Microsoft.JSInterop;
using MudBlazor;
using System.Net.Http.Json;
using System.Net.Http;

namespace HRMS.Pages
{
    public partial class Completedproject
    {
        #region veriable
       
        private string _searchString = string.Empty;
        private string _noResultsMessage = string.Empty;
        List<HRMS.Model.Project> ProjectEvents = new List<HRMS.Model.Project>();
        #endregion

        //Go back Function
        private async Task GoBack()
        {
            await JSRuntime.InvokeVoidAsync("history.back");
        }

        //OnInitializedAsync function
        protected override async Task OnInitializedAsync()
        {
            await CompletedProject();
        }

        //search Function
        private Func<HRMS.Model.Project, bool> _quickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            var combinedString = $"{x.ProjectName} {x.CreatedDate} {x.DueDate} {x.ClientName}";

            if (combinedString.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            var hasResults = ProjectEvents.Any(e => $"{e.ProjectName} {e.CreatedDate} {e.DueDate} {e.ClientName}".Contains(_searchString, StringComparison.OrdinalIgnoreCase));

            if (!hasResults)
            {
                _noResultsMessage = "No Results found";
            }

            return hasResults;
        };


        //Dialog for TeamMembers
        private void OpenDialog(long id)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters { ["Id"] = id };
            DialogService.Show<TeamMembers>("", parameters, options);
        }

        //Api For Completed Project
        public async Task CompletedProject()
        {
            const string apiUrl = "api/Project";
            using var client = HttpClientFactory.CreateClient("API");

            var response = await client.GetAsync(apiUrl).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.Project>>();
                ProjectEvents = json;
                StateHasChanged();
            }
            else
            {
                Console.WriteLine("Project Loadind Failed");
            }
        }
    }
}