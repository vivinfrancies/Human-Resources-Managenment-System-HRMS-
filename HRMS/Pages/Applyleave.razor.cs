using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;
using System.Net.Http;
using HRMS.Model;
using Microsoft.JSInterop;
using System.Text.Json;

namespace HRMS.Pages
{
	public partial class Applyleave
	{
		#region properties
		HRMS.Model.Leave leaveEvent = new HRMS.Model.Leave();

		List<HRMS.Model.LeaveTypeDetails> leaveType = new List<HRMS.Model.LeaveTypeDetails>();
		HRMS.Model.LeaveTypeDetails LeaveId = new HRMS.Model.LeaveTypeDetails();

		MudForm form;
		public bool submit;
		#endregion

		#region leavetype[Dropdown]
		//getting the leavetype for the dropdown
		public async Task LeaveType()
		{
			try
			{
				const string apiUrl = "api/Leave/LeaveTypeDetails";
				using var client = HttpClientFactory.CreateClient("API");
				var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
				if (response.IsSuccessStatusCode)
				{
					var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.LeaveTypeDetails>>();
					leaveType = json;

					StateHasChanged();
				}
				else
				{
					Snackbar.Add("Saved Failed");

				}
			}
			catch (Exception ex)
			{
				Snackbar.Add($"Exception:{ex.Message}");
			}
		}
		#endregion

		#region apply leave
		//posting the applied leave
		public async Task Saves()
		{
			try
			{
				if (leaveEvent.Reason != null && leaveEvent.Detailedreason != null)
				{
					var selectedLeaveType = leaveType.FirstOrDefault(x => x.Id == LeaveId.Id);
					leaveEvent.LeaveType = selectedLeaveType.Id;
					leaveEvent.Id = User.Id;
					const string apiUrl = "api/Leave";
					using var client = HttpClientFactory.CreateClient("API");
					var response = await client.PostAsJsonAsync(apiUrl, leaveEvent).ConfigureAwait(false);
					if (response.IsSuccessStatusCode)
					{
						Cancel();
						/*NavigationManager.NavigateTo("/approve");*/
						Snackbar.Add("Saved Successfully");

					}
					else
					{
						Snackbar.Add("Failed");
					}
				}
				else
				{
					Snackbar.Add("Fill the Details");
				}
			}
			catch (Exception ex)
			{
                Snackbar.Add($"Exception:{ex.Message}");
            }

		}
		#endregion

		protected override async Task OnInitializedAsync()
		{
			var userJson = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "user");
			User = JsonSerializer.Deserialize<LoginDetails>(userJson);
			await LeaveType();
		}

		[CascadingParameter] MudDialogInstance MudDialog { get; set; }

        void Submit() => MudDialog.Close(DialogResult.Ok(true));
        void Cancel() => MudDialog.Cancel();

	}
}