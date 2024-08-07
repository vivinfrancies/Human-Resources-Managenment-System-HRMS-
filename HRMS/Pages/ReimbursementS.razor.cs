using ApexCharts;
using HRMS.Model;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;
using System.Net.Http.Json;
using System.Text.Json;

namespace HRMS.Pages
{
	public partial class ReimbursementS
	{

		public bool emptytext;

		public bool success;
		public string selectedexpense { get; set; }

		MudForm form;

		//File save
		private IBrowserFile? Billfile { get; set; }



		HRMS.Model.Reimbursement reimbursement = new HRMS.Model.Reimbursement();

		List<HRMS.Model.Reimbursement> Expensetype = new List<HRMS.Model.Reimbursement>();

		List<HRMS.Model.Reimbursement> rolewithid = new List<HRMS.Model.Reimbursement>();

		List<HRMS.Model.Reimbursement> expenselimitview = new List<HRMS.Model.Reimbursement>();


		//To Load Page Initial
		protected override async Task OnInitializedAsync()
		{
			var userJson = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "user");
			User = JsonSerializer.Deserialize<LoginDetails>(userJson);

			await getroleid();

			//Get EXPENSE LIMIT LIST from API
			const string apiUrl2 = "api/CompanyPolicy/Expense Limit Table";
			using var client2 = HttpClientFactory.CreateClient("API");

			var response2 = await client2.GetAsync(apiUrl2).ConfigureAwait(false);

			if (response2.IsSuccessStatusCode)
			{
				var json = await response2.Content.ReadFromJsonAsync<List<HRMS.Model.Reimbursement>>();
				expenselimitview = json;
				StateHasChanged();
			}
			else
			{
				Snackbar.Add("Failed");
			}
			//To get expense type id
			try
			{
				const string apiUrl1 = "api/ApplyReimburstment/Expense Types";
				using var client1 = HttpClientFactory.CreateClient("API");

				var response1 = await client1.GetAsync(apiUrl1).ConfigureAwait(false);

				if (response1.IsSuccessStatusCode)
				{
					var json = await response1.Content.ReadFromJsonAsync<List<HRMS.Model.Reimbursement>>();
					Expensetype = json;
					StateHasChanged();
				}
				else
				{
					Snackbar.Add("Failed");
				}
			}
			catch (Exception ex)
			{
				Snackbar.Add(ex.Message, Severity.Error);
			}
		}


		//To Get id for Role
		public async Task getroleid()
		{
			try
			{
				const string apiurl = "api/RolenDesignation/Role";
				using var client = HttpClientFactory.CreateClient("API");
				var response = await client.GetAsync(apiurl).ConfigureAwait(false);
				if (response.IsSuccessStatusCode)
				{
					var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.Reimbursement>>();
					rolewithid = json;
					StateHasChanged();
				}
				else
				{
					Snackbar.Add("Failed");
				}

			}
			catch (Exception ex)
			{
				Snackbar.Add(ex.Message, Severity.Error);
			}
		}

		//Submit Form Function
		public async Task Applyreimburstment()
		{


			var expenserow = Expensetype.FirstOrDefault(x => x.Expensetypes == selectedexpense);

			var rolerow = rolewithid.FirstOrDefault(x => x.roleName == User.Role);

			var internetlimit = expenselimitview.FirstOrDefault(x => x.Roles == User.Role);

			reimbursement.EmpId = User.Id;

			reimbursement.Expense = expenserow.Id;

			reimbursement.Role = rolerow.roleId;
			//

			if (reimbursement.Expense == 1)
			{
				if (reimbursement.BillAmount > internetlimit.InternetLimit)
				{
					Snackbar.Add($"Enter Valid Limit (Below ₹ {internetlimit.InternetLimit})", Severity.Warning);
					reimbursement.Expense = null;
				}
				else
				{
					await Postclaim();
				}
			}
			else if (reimbursement.Expense == 2)
			{
				if (reimbursement.BillAmount > internetlimit.TravelLimit)
				{

					Snackbar.Add($"Enter Valid Limit (Below ₹ {internetlimit.TravelLimit})", Severity.Warning);
					reimbursement.Expense = null;

				}
				else
				{
					await Postclaim();
				}
			}
		}
		//POST Reimburstment Claim 
		public async Task Postclaim()
		{
			try
			{
				const string apiUrl = "api/ApplyReimburstment/Apply Reimbursement Claim";
				using var client = HttpClientFactory.CreateClient("API");
				var response = await client.PostAsJsonAsync(apiUrl, reimbursement).ConfigureAwait(false);
				if (response.IsSuccessStatusCode)
				{
					Snackbar.Add("Reimburstment Claim Successfully Submitted", Severity.Success);
				}
				else
				{
					Snackbar.Add("Unable to Submit Reimburstment Claim");
				}
			}
			catch (Exception ex)
			{
				Snackbar.Add(ex.Message);
			}


			NavigationManager.NavigateTo("/ReimbursementHistory");
		}

		//Search expense
		private async Task<IEnumerable<string>> SearchExpense(string value)
		{

			if (string.IsNullOrEmpty(value))
				return Expensetype.Select(x => x.Expensetypes);

			return Expensetype
				.Where(x => x.Expensetypes.Contains(value, StringComparison.OrdinalIgnoreCase))
				.Select(x => x.Expensetypes);
		}

		//GoBack
		private async Task GoBack()
		{
			await JSRuntime.InvokeVoidAsync("history.back");
		}
	}
}