using Microsoft.JSInterop;
using MudBlazor;
using System.Net.Http.Json;
using System.Text.Json;

namespace HRMS.Pages
{

	public partial class Voluntaryresignationform
	{

		
		public bool ok1 { get; set; } = false;
		MudForm form;
		bool success;

		private User _user;

		private async Task LoadUserFromLocalStorage()
		{
			var userJson = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "user");
			if (!string.IsNullOrEmpty(userJson))
			{
				_user = JsonSerializer.Deserialize<User>(userJson);
			}
		}

		public class User
		{
			public int Id { get; set; }
			public string UserName { get; set; }
			public string Password { get; set; }
			public string Role { get; set; }
			public string NewPassword { get; set; }
			public string Firstname { get; set; }
			public string Profileimage { get; set; }
			public string Fullname { get; set; }
		}
		protected override async Task OnInitializedAsync()
		{
			await LoadUserFromLocalStorage();
			await resignationformget(_user.UserName);
			await Reason();
			await GetEmployeeId();
		}
		private async Task GoBack()
		{
			await JSRuntime.InvokeVoidAsync("history.back");
		}
		List<HRMS.Model.EmployeeList> Resignationform = new List<HRMS.Model.EmployeeList>();
		HRMS.Model.EmployeeList resignform = new HRMS.Model.EmployeeList();
		HRMS.Model.ResignationTable resigndatafill { get; set; } = new HRMS.Model.ResignationTable();
		List<HRMS.Model.Reason> resignReason = new List<HRMS.Model.Reason>();
		HRMS.Model.Reason vaildreason = new HRMS.Model.Reason();
		public async Task Reason()
		{
			string apiUrl = $"api/Resignation/reasonview";
			using var client = HttpClientFactory.CreateClient("API");

			var response = await client.GetAsync(apiUrl).ConfigureAwait(false);

			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.Reason>>();
				resignReason = json;

				StateHasChanged();
			}
			else
			{
				Snackbar.Add("Failed");
			}
		}
		public async Task GetEmployeeId()
		{
			var id = Resignationform.FirstOrDefault(x => x.Employeecode == _user.UserName).EmpId;
			resigndatafill.EmpId = id;

		}
		public async Task resignationformget(string Employeeid)
		{
			string apiUrl = $"api/EmployeeDetailsTable/Employeeoveralldetails/{Employeeid}";
			using var client = HttpClientFactory.CreateClient("API");

			var response = await client.GetAsync(apiUrl).ConfigureAwait(false);

			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.EmployeeList>>();
				Resignationform = json;

				StateHasChanged();
			}
			else
			{
				Snackbar.Add("Failed");
			}
		}
		public async Task Save()
		{
			var apiUrl = "api/Resignation";
			using var client = HttpClientFactory.CreateClient("API");
			var response = await client.PostAsJsonAsync(apiUrl, resigndatafill).ConfigureAwait(false);
			if (response.IsSuccessStatusCode)
			{
				Snackbar.Add("Saved Successfully", Severity.Success);
				NavigationManager.NavigateTo("/dash");
			}
			else
			{
				Snackbar.Add("Failed", Severity.Error);
			}
		}
	}
}