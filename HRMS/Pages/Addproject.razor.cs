using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.Net.Http.Json;
using System.Net.Http;
using static MudBlazor.Colors;
using HRMS.Model;

using System.Security.Cryptography;
using static MudBlazor.CategoryTypes;

namespace HRMS.Pages
{
    public partial class Addproject
    {
        #region variables
        Project Projectvalue = new Project();

        private IEnumerable<string> selectedTeamMembers { get; set; } = new List<string>();
        private string SelectedTeamMembersDisplay => string.Join(", ", selectedTeamMembers);

        List<HRMS.Model.EmployeeList> EmployeeListEvents = new List<HRMS.Model.EmployeeList>();

        List<HRMS.Model.Project> ProjectEvents = new List<HRMS.Model.Project>();
        private string value { get; set; } = string.Empty;
        private IEnumerable<string> options { get; set; } = new HashSet<string>();
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        void Cancel() => MudDialog.Close(DialogResult.Ok(true));
        bool success;
        IList<IBrowserFile> files = new List<IBrowserFile>();
        private void UploadFiles(IBrowserFile file)
        {
            files.Add(file);
        }
        //task
        private bool isTaskHidden = true;
        private string Btnname = "add Task";
        MudForm form;

        string fullname { get; set; }
        #endregion

        private void ToggleTaskVisibility()
        {
            if (isTaskHidden == false)
            {
                Btnname = "add Task";
            }
            else
            {
                Btnname = "close Task";
            }

            isTaskHidden = !isTaskHidden;
        }

        private async Task EmployeeDetailsTable()
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
        protected override async Task OnInitializedAsync()
        {
            await EmployeeDetailsTable();
        }

        private async Task<IEnumerable<string>> Search1(string value)
        {
            var members = new List<(int EmpId, string Fullname)>();

            for (int i = 0; i < EmployeeListEvents.Count; i++)
            {
                var Emp = EmployeeListEvents[i];
                var fullname = $"{Emp.Firstname1} {Emp.Middlename1} {Emp.Lastname1}";
                int empid = Emp.EmpId;
                members.Add((empid, fullname));
            }

            var filteredMembers = members.Where(member => selectedTeamMembers.Contains(member.Fullname));

            if (string.IsNullOrEmpty(value))
                return filteredMembers.Select(member => $"{member.Fullname}");

            return filteredMembers
                .Where(member => member.Fullname.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(member => $"{member.Fullname}");
        }

        public async Task Submit()
        {
            if (Projectvalue.TeamMembers != null)
            {
                foreach (var x in selectedTeamMembers)
                {
                    foreach (var emp in EmployeeListEvents)
                    {
                        if (x == Projectvalue.TeamMembers)
                        {
                            Projectvalue.EmpId = emp.EmpId;
                            Projectvalue.Role = emp.DesignationId;
                        }
                    }
                }
            }

            //Adding Project
            string apiUrl = "api/Project/Project";
            using var client = HttpClientFactory.CreateClient("API");
            var response = await client.PostAsJsonAsync(apiUrl, Projectvalue).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                Snackbar.Add("Project added Successfully", Severity.Success);
            }
            else
            {
                Snackbar.Add("Failed", Severity.Error);
            }


            foreach (var x in selectedTeamMembers)
            {
                foreach (var emp in EmployeeListEvents)
                {
                    var fullname = $"{emp.Firstname1} {emp.Middlename1} {emp.Lastname1}";

                    if (x == fullname)
                    {
                        Projectvalue.EmpId = emp.EmpId;
                        Projectvalue.Role = emp.DesignationId;
                        //Adding TeamMembers
                        string apiUrl1 = "api/Project/ProjectMembers";
                        using var client1 = HttpClientFactory.CreateClient("API");
                        var response1 = await client1.PostAsJsonAsync(apiUrl1, Projectvalue).ConfigureAwait(false);
                        if (response1.IsSuccessStatusCode)
                        {
                            Snackbar.Add("Project added Successfully", Severity.Success);
                        }
                        else
                        {
                            Console.WriteLine("Adding Members Failed", Severity.Error);
                        }
                    }
                }
            }
            MudDialog.Close(DialogResult.Ok(true));
        }
    }
}