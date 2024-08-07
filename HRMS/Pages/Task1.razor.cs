using HRMS.Model;
using Microsoft.JSInterop;
using MudBlazor;
using System.Net.Http.Json;
using System.Text.Json;

namespace HRMS.Pages
{
    public partial class Task1
    {
        //For Authentication
        private IEnumerable<HRMS.Model.Tasks1> GetFilteredTasks()
        {
            if (User.RoleId == 1)
            {
                return TasksEvents.Where(x => x.projectstatus == true);
            }
            else
            {
                return TasksEvents.Where(x => x.projectstatus == true && x.EmpId == User.Id);
            }
        }
        private async Task GoBack()
        {
            await JSRuntime.InvokeVoidAsync("history.back");
        }

        List<HRMS.Model.Tasks1> TasksEvents = new List<HRMS.Model.Tasks1>();
        protected override async Task OnInitializedAsync()
        {
            var userJson = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "user");
            User = JsonSerializer.Deserialize<LoginDetails>(userJson);
            await Taskdetails();
        }


        public async Task Taskdetails()
        {
            const string apiUrl = "api/Tasks";
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

        public void DeleteDialog(long id)
        {

            TaskId = id;
            Deletevisible = true;
        }

        public void SucessDialog(long id)
        {

            TaskId = id;
            visible = true;
        }

        void Cancel() => visible = false;
        public bool visible;
        public bool Deletevisible;
        private long TaskId;
        private DialogOptions dialogOptions = new() { FullWidth = true, CloseOnEscapeKey = true };
        public async Task DeleteConfirmed()
        {
            await Delete(TaskId);
            Deletevisible = false;
        }
        public async Task StatusConfirmed()
        {
            await Status(TaskId);
            visible = false;
        }

        //API For Delete
        public async Task Delete(long id)
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

        //API For Sucess
        public async Task Status(long id)
        {
            string apiUrl = $"api/Tasks/TaskStatus/{id}";
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
    }
}