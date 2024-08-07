using Microsoft.JSInterop;
using MudBlazor;
using System.Net.Http.Json;
using System.Net.Http;
using HRMS.Model;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace HRMS.Pages
{
    public partial class Projects
    {
        #region variables
        List<HRMS.Model.Project> ProjectEvents = new List<HRMS.Model.Project>();
        List<HRMS.Model.Project> TeamMembers = new List<HRMS.Model.Project>();
        List<HRMS.Model.Project> Login = new List<HRMS.Model.Project>();

        Project Projectvalue = new Project();
        List<HRMS.Model.Tasks1> TasksEvents = new List<HRMS.Model.Tasks1>();
        List<HRMS.Model.EmployeeList> EmployeeListEvents = new List<HRMS.Model.EmployeeList>();
        private IEnumerable<string> options { get; set; } = new HashSet<string>();
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        //task
        private bool isTaskHidden = true;
        private string Btnname = "Show Task";

        private bool isTeamMemberHidden = true;
        private string memberbtn = "Show Members";
        public bool visible;
        public bool statusvisible;
        public bool Mdoulevisible;
        public bool visibleProject;
        private long projectId;
        private long ModuleId;

        private DialogOptions dialogOptions = new() { FullWidth = true, CloseOnEscapeKey = true };

        Searchproject Search = new Searchproject();
        #endregion


        //Go Back Function
        private async Task GoBack()
        {
            await JSRuntime.InvokeVoidAsync("history.back");
        }

        //Edit Module Dialog
        public void EditModule(long id)
        {
            try
            {
                foreach (var x in ProjectEvents)
                {
                    if (id == x.Id)
                    {
                        Projectvalue.Id = x.Id;
                        Projectvalue.Module = x.Module;
                    }
                }
                ModuleId = id;
                Mdoulevisible = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        //Closing Dialog
        void Cancel()
        {
            visible = false;
            Mdoulevisible = false;
            visibleProject = false;
        }

        //Creating New Project
        private void OpenDialog()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            DialogService.Show<Addproject>("Create Project", options);
        }

        //To Open TeamMembers Of Each Project
        private void OpenTeamMembers(long id)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters { ["Id"] = id };
            DialogService.Show<TeamMembers>("", parameters, options);
            Console.WriteLine(parameters);
        }

        //To Open Inline Dialog 
        public void Dialog(long id)
        {
            projectId = id;
            visible = true;
        }

        //To Open Inline Dialog for Completed status
        public void Completed(long id)
        {
            projectId = id;
            statusvisible = true;
        }

        //To Open Edit Dialog
        public async Task EditProject(long id)
        {
            try
            {
                projectId = id;
                visibleProject = true;

                foreach (var x in ProjectEvents)
                {
                    if (id == x.Id)
                    {
                        Projectvalue.Id = x.Id;
                        Projectvalue.ProjectName = x.ProjectName;
                        Projectvalue.ClientName = x.ClientName;
                        Projectvalue.DueDate = x.DueDate;
                        Projectvalue.Description = x.Description;
                        Projectvalue.Module = x.Module;
                        Projectvalue.ClientID = x.ClientID;
                    }
                }

                await ProjectMembers();
                await Taskdetails();
                await project();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        public async Task ProjectMembers()
        {
            //Project Members
            string apiUrl = $"api/Project/ProjectMembers/{projectId}";
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


        //Confirmation For Delete
        public async Task DeleteConfirmed()
        {
            await Delete(projectId);
            visible = false;
        }

        //Confirmation For Delete
        public async Task UpdateProjectConfirmed()
        {
            await UpdateProjec(projectId);
            statusvisible = false;
        }

        //Update Module  for Members
        public async Task UpdateConfirmed()
        {
            await ModuleUpdate(ModuleId);
            Mdoulevisible = false;
        }

        //Function On Loading
        protected override async Task OnInitializedAsync()
        {
            var userJson = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "user");
            User = JsonSerializer.Deserialize<LoginDetails>(userJson);

            await logindetails();
            await project();
            await EmployeeDetailsTable();
        }



        //Task Toggle
        private void ToggleTaskVisibility()
        {
            if (isTaskHidden == false)
            {
                Btnname = "Show Task";
            }
            else
            {
                Btnname = "close Task";
            }

            isTaskHidden = !isTaskHidden;
        }

        //TeamMembers Toggle
        private void ToggleMemberVisibility()
        {
            if (isTeamMemberHidden == false)
            {
                memberbtn = "Show Members";
            }
            else
            {
                memberbtn = "close Members";
            }

            isTeamMemberHidden = !isTeamMemberHidden;
        }

        //API for Loading Project
        public async Task project()
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
                    Console.WriteLine("Project Loading Failed");
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

        }

        //API For Getting Employee Details
        private async Task EmployeeDetailsTable()
        {

            try
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
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

        }

        //API Project Task
        public async Task Taskdetails()
        {
            try
            {
                string apiUrl = $"api/Tasks/{projectId}";
                using var client = HttpClientFactory.CreateClient("API");

                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.Tasks1>>();
                    TasksEvents = json;
                    StateHasChanged();
                }
                else
                {
                    Snackbar.Add("Failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        //API for Login
        public async Task logindetails()
        {
            try
            {
                string apiUrl = $"api/Project/ProjectLogin/{User.Id}";
                using var client = HttpClientFactory.CreateClient("API");

                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.Project>>();
                    Login = json;
                    StateHasChanged();
                }
                else
                {
                    Snackbar.Add("Failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }

        }

        //API For Update  Project
        public async Task UpdateProjec(long id)
        {
            string apiUrl = $"api/Project/UpdateProjectStatus/{id}";
            using var client = HttpClientFactory.CreateClient("API");
            var response = await client.PutAsJsonAsync(apiUrl, ProjectEvents).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                Snackbar.Add("Project Completed Successfully", Severity.Success);
            }
            else
            {
                Snackbar.Add("Failed", Severity.Warning);
            }
            await project();
        }

        //API For Delete Project
        public async Task Delete(long id)
        {
            string apiUrl = $"api/Project/Delete/{id}";
            using var client = HttpClientFactory.CreateClient("API");
            var response = await client.PutAsJsonAsync(apiUrl, ProjectEvents).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                Snackbar.Add("Project Deleted Successfully", Severity.Error);
            }
            else
            {
                Snackbar.Add("Failed", Severity.Warning);
            }
            await project();
        }


        //API For Delete Task
        public async Task Deletetask(long id)
        {
            string apiUrl = $"api/Tasks/Delete/{id}";
            using var client = HttpClientFactory.CreateClient("API");
            var response = await client.PutAsJsonAsync(apiUrl, TasksEvents).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                Snackbar.Add("Task Deleted Successfully", Severity.Error);
            }
            else
            {
                Snackbar.Add("Failed", Severity.Warning);
            }
            await Taskdetails();
        }

        //API For Delete TeamMembers
        public async Task DeleteTeamMembers(long id)
        {
            string apiUrl = $"api/Project/TeamMemberDelete/{id}";
            using var client = HttpClientFactory.CreateClient("API");
            var response = await client.PutAsJsonAsync(apiUrl, ProjectEvents).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                Snackbar.Add("Team Member Successfully", Severity.Error);
            }
            else
            {
                Snackbar.Add("Failed", Severity.Warning);
            }
            await ProjectMembers();
        }

        //API For Updateproject
        public async Task ProjectUpdate(long id)
        {
            string apiUrl = $"api/Project/Project/{id}";
            using var client = HttpClientFactory.CreateClient("API");
            var response = await client.PutAsJsonAsync(apiUrl, Projectvalue).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                Snackbar.Add("Module Updaeted Successfully", Severity.Success);
            }
            else
            {
                Snackbar.Add("Failed", Severity.Warning);
            }

            await ClientUpdate(Projectvalue.ClientID);
            visibleProject = false;
            await project();
        }

        //API For Update Client
        public async Task ClientUpdate(long id)
        {
            string apiUrl = $"api/Project/Client/{id}?clientname={Projectvalue.ClientName}";
            using var client = HttpClientFactory.CreateClient("API");
            var response = await client.PutAsJsonAsync(apiUrl, Projectvalue).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                Snackbar.Add("Module Updaeted Successfully", Severity.Success);
            }
            else
            {
                Snackbar.Add("Failed", Severity.Warning);
            }
        }

        //API For Update Module
        public async Task ModuleUpdate(long id)
        {
            string apiUrl = $"api/Project/TeamMemberModuleUpdate/{id}?module={Projectvalue.Module}";
            using var client = HttpClientFactory.CreateClient("API");
            var response = await client.PutAsJsonAsync(apiUrl, ProjectEvents).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                Snackbar.Add("Module Updaeted Successfully", Severity.Success);
            }
            else
            {
                Snackbar.Add("Failed", Severity.Warning);
            }
            await ProjectMembers();
        }
    }
}