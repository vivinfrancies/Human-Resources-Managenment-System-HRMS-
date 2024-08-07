using HRMS.Model;
using Microsoft.JSInterop;
using MudBlazor;
using System.Net.Http.Json;
using System.Text.Json;

namespace HRMS.Pages
{
	public partial class ReimbursementHistory
	{
		private string _searchString;


		HRMS.Model.Reimbursement reimbursement = new HRMS.Model.Reimbursement();

		List<HRMS.Model.Reimbursement> Personalhistory = new List<HRMS.Model.Reimbursement>();


		protected override async Task OnInitializedAsync()
		{
			var userJson = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "user");
			User = JsonSerializer.Deserialize<LoginDetails>(userJson);
			await loadhistory();
		}

		public async Task loadhistory()
		{
			string apiUrl = $"api/ApplyReimburstment/PersonalClaimHistory/{User.UserName}";
			using var client = HttpClientFactory.CreateClient("API");

			var response = await client.GetAsync(apiUrl).ConfigureAwait(false);

			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.Reimbursement>>();
				Personalhistory = json;
				StateHasChanged();
			}
			else
			{
				Snackbar.Add("Failed");
			}
		}

		// quick filter - filter globally across multiple columns with the same input
		private Func<HRMS.Model.Reimbursement, bool> _quickFilter => x =>
		{
			if (string.IsNullOrWhiteSpace(_searchString))
				return true;

			if (x.Expensetypes.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
				return true;

			if (x.Status.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
				return true;

			return false;
		};


		//ExpenseColumn

		private Func<HRMS.Model.Reimbursement, String> Expensebg => y =>
		{
			string style = "";

			if (y.Status == "Approved")

				style += "color:#00ef63";

			else if (y.Status == "Pending")

				style += "color:#ffaa00";

			else if (y.Status == "Rejected")

				style += "color:#f21c0d";

			return style;
		};

		//GoBack

		private async Task GoBack()
		{
			await JSRuntime.InvokeVoidAsync("history.back");
		}
	}
}