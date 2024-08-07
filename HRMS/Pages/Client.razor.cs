using Microsoft.JSInterop;
using MudBlazor;
using System.Net.Http.Json;
using System.Net.Http;

namespace HRMS.Pages
{
    public partial class Client
    {
        private async Task GoBack()
        {
            await JSRuntime.InvokeVoidAsync("history.back");
        }


        private string _searchString = string.Empty;

        private string _noResultsMessage = string.Empty;
        private Func<HRMS.Model.Project, bool> _quickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;
            if ($"{x.ProjectName} {x.CreatedDate} {x.DueDate} {x.ClientName}".Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            var hasResults = ProjectEvents.Any(e => $"{x.ProjectName} {x.CreatedDate} {x.DueDate} {x.ClientName}".Contains(_searchString, StringComparison.OrdinalIgnoreCase));
            if (!hasResults)
            {
                _noResultsMessage = "No Results found";
            }

            return hasResults;
        };





        List<HRMS.Model.Project> ProjectEvents = new List<HRMS.Model.Project>();
        protected override async Task OnInitializedAsync()
        {
            await CompletedProject();
        }

        public async Task CompletedProject()
        {
            try
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
                for (int i = 0; i < ProjectEvents.Count; i++)
                {
                    ProjectEvents[i].Sno = i + 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, Severity.Error);
            }
        }
    }
}