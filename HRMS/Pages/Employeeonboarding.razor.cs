
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;
using System.Net.Http.Json;
using System.Text.Json;

namespace HRMS.Pages
{
	public partial class Employeeonboarding
	{

		DateTime? date = DateTime.Today;
		public bool EmployeeDetailsHide = false;
		public bool Employeepersonalhide = true;
		public bool EmployeeAccounthide = true;
		
		MudForm form;
		bool success;
		MudForm form2;
		bool success2;
		MudForm form3;
		bool success3;
		string roleid;
		string Designationid;
		private IBrowserFile File3 { get; set; }



		void onclick()
		{
			EmployeeDetailsHide = true;
			Employeepersonalhide = false;
			EmployeeAccounthide = true;
		}
		void onclick2()
		{
			EmployeeDetailsHide = true;
			Employeepersonalhide = true;
			EmployeeAccounthide = false;
		}


		private async Task GoBack()
		{
			await JSRuntime.InvokeVoidAsync("history.back");
		}
		
		HRMS.Model.EmployeeDetailsTable employeedetails = new HRMS.Model.EmployeeDetailsTable();

		List<HRMS.Model.RolenDesignation> rolenDesignations = new List<HRMS.Model.RolenDesignation>();

		List<HRMS.Model.RolenDesignation> Designations = new List<HRMS.Model.RolenDesignation>();
		HRMS.Model.RolenDesignation roles = new HRMS.Model.RolenDesignation();

		protected override async Task OnInitializedAsync()
		{



			try
			{
				string apiUrl = "api/RolenDesignation/Role";
				using var client = HttpClientFactory.CreateClient("API");

				var response = await client.GetAsync(apiUrl).ConfigureAwait(false);

				if (response.IsSuccessStatusCode)
				{
					var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.RolenDesignation>>();
					rolenDesignations = json;
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
			try
			{
				const string apiUrl = "api/RolenDesignation/Designation";
				using var client = HttpClientFactory.CreateClient("API");

				var response = await client.GetAsync(apiUrl).ConfigureAwait(false);

				if (response.IsSuccessStatusCode)
				{
					var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.RolenDesignation>>();
					Designations = json;
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

		public async Task Saves()
		{
			try
			{
				const string apiUrl = "api/EmployeeDetailsTable";
				using var client = HttpClientFactory.CreateClient("API");
				var response = await client.PostAsJsonAsync(apiUrl, employeedetails).ConfigureAwait(false);
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
			catch (Exception ex)
			{
				Snackbar.Add(ex.Message);
			}
		}

	}
}