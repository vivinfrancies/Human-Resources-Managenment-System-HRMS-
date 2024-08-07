using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;
using System.Net.Http;
using Microsoft.JSInterop;
using System.Text.Json;

namespace HRMS.Pages
{
    public partial class TeamMembers
    {
        #region List
        List<HRMS.Model.Project> ProjectEvents = new List<HRMS.Model.Project>();
        #endregion

        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        void Cancel() => MudDialog.Close(DialogResult.Ok(true));


        protected override async Task OnInitializedAsync()
        {
            await ProjectMembers();
        }


        [Parameter]
        public long Id { get; set; }

        private async Task ProjectMembers()
        {
            string apiUrl = $"api/Project/ProjectMembers/{Id}";
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
                Console.WriteLine("Project Members Loading Failed");
            }
        }
    }
}