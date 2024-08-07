using HRMS.Model;
using MudBlazor;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using System.Xml.Linq;
using Microsoft.JSInterop;
using System.Net.Http;

namespace HRMS.Pages
{
	public partial class CompanyPolicy
	{
		public bool Visible;

		public bool Visible2;

		HRMS.Model.Reimbursement selectedPolicy;

		HRMS.Model.Reimbursement Reimbursement = new HRMS.Model.Reimbursement();

		List<HRMS.Model.Reimbursement> leavelimitview = new List<HRMS.Model.Reimbursement>();

		List<HRMS.Model.Reimbursement> expenselimitview = new List<HRMS.Model.Reimbursement>();


		/// Get LEAVE LIST from API
		protected override async Task OnInitializedAsync()
		{
			const string apiUrl = "api/CompanyPolicy/Leave Limit Table";
			using var client = HttpClientFactory.CreateClient("API");

			//
			var userJson = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "user");
			User = JsonSerializer.Deserialize<LoginDetails>(userJson);

			var response = await client.GetAsync(apiUrl).ConfigureAwait(false);

			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.Reimbursement>>();
				leavelimitview = json;
				StateHasChanged();
			}
			else
			{
				Snackbar.Add("Failed");
			}


			//Get EXPENSE LIMIT LIST from API
			const string apiUrl2 = "api/CompanyPolicy/Expense Limit Table";
			using var client2 = HttpClientFactory.CreateClient("API");

			var response2 = await client2.GetAsync(apiUrl2).ConfigureAwait(false);

			if (response.IsSuccessStatusCode)
			{
				var json = await response2.Content.ReadFromJsonAsync<List<HRMS.Model.Reimbursement>>();
				expenselimitview = json;
				StateHasChanged();
			}
			else
			{
				Snackbar.Add("Failed");
			}

		}


		public DialogOptions dialogOptions = new() { FullWidth = true };

		//To Open Dialogue Box (LeaveLimit)	
		public void OpenDialog(string roles)
		{
			Visible = true;
			selectedPolicy = leavelimitview.FirstOrDefault(x => x.Roles == roles);
			Reimbursement.LeaveLimit = selectedPolicy.LeaveLimit;

		}

		//TO Update LeaveLimit in DB
		public async void Submit()
		{
			Visible = false;

			if (selectedPolicy != null)
			{

				string apiUrl5 = $"api/CompanyPolicy/Update Leave Limit / {selectedPolicy.Id}?leaveLimit={Reimbursement.LeaveLimit}&modifiedBy={User.Id}";
				using var client = HttpClientFactory.CreateClient("API");
				var response = await client.PutAsJsonAsync(apiUrl5, Reimbursement).ConfigureAwait(false);
				if (response.IsSuccessStatusCode)
				{

					Snackbar.Add($"{selectedPolicy.Roles} Leave Limit Updated", Severity.Success);
					selectedPolicy.LeaveLimit = Reimbursement.LeaveLimit;
					StateHasChanged();
				}
				else
				{
					Snackbar.Add("Failed");
				}

			}
			else
			{
				throw new Exception("No matching Policy found");
			}
		}

		//To Open Dialogue Box (ExpenseLimit)	
		public void OpenDialog2(string roless)
		{
			Visible2 = true;
			selectedPolicy = expenselimitview.FirstOrDefault(y => y.Roles == roless);
			Reimbursement.InternetLimit = selectedPolicy.InternetLimit;
			Reimbursement.TravelLimit = selectedPolicy.TravelLimit;
		}


		//TO Update ExpenseLimit in DB
		public async void Submitt()
		{
			Visible2 = false;

			if (selectedPolicy != null)
			{
				string apiurlexpense = $"api/CompanyPolicy/Update Expense Limit /{selectedPolicy.Id}?internetLimit={Reimbursement.InternetLimit}&travelLimit={Reimbursement.TravelLimit}&modifiedBy={User.Id}";
				using var client = HttpClientFactory.CreateClient("API");
				var response = await client.PutAsJsonAsync(apiurlexpense, Reimbursement).ConfigureAwait(false);
				if (response.IsSuccessStatusCode)
				{
					Snackbar.Add($"{selectedPolicy.Roles} Expense's Updated", Severity.Success);
					selectedPolicy.InternetLimit = Reimbursement.InternetLimit;
					selectedPolicy.TravelLimit = Reimbursement.TravelLimit;
					StateHasChanged();
				}
				else
				{
					Snackbar.Add("Failed");
				}
			}
		}

		//GoBack

		private async Task GoBack()
		{
			await JSRuntime.InvokeVoidAsync("history.back");
		}
	}
}