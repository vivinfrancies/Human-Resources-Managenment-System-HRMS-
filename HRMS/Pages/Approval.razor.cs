using ApexCharts;
using HRMS.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using MudBlazor;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;
using static MudBlazor.CategoryTypes;

namespace HRMS.Pages;

public partial class Approval
{
	#region Properties
	HRMS.Model.Leave leaveEvent1 = new HRMS.Model.Leave();

	List<HRMS.Model.LeaveTypeDetails> leaveType = new List<HRMS.Model.LeaveTypeDetails>();

	MudForm form;
	public bool submit;

	List<HRMS.Model.ListModel> SingleLeaveList = new List<HRMS.Model.ListModel>();
	HRMS.Model.ListModel Employeeleave = new HRMS.Model.ListModel();

	List<HRMS.Model.Chartmodel> chartmodel = new List<HRMS.Model.Chartmodel>();
	HRMS.Model.Chartmodel chartlist = new HRMS.Model.Chartmodel();

	List<HRMS.Model.LeaveTypeDetails> leaveTypeapprove = new List<HRMS.Model.LeaveTypeDetails>();
	HRMS.Model.LeaveTypeDetails LeaveId = new HRMS.Model.LeaveTypeDetails();

	HRMS.Model.Leave leaveEvent = new HRMS.Model.Leave();

	List<HRMS.Model.Dashboard> AnnualLeaveCount = new List<HRMS.Model.Dashboard>();
	public bool value;
	public bool value1;

	#endregion

	#region apply leave
	//posting the applied leave
	public async Task Saves()
	{
		try
		{
			if (leaveEvent1.Reason != null && leaveEvent1.Detailedreason != null)
			{
				var selectedLeaveType = leaveTypeapprove.FirstOrDefault(x => x.Id == LeaveId.Id);
				leaveEvent1.LeaveType = selectedLeaveType.Id;
				leaveEvent1.Id = User.Id;
				const string apiUrl = "api/Leave";
				using var client = HttpClientFactory.CreateClient("API");
				var response = await client.PostAsJsonAsync(apiUrl, leaveEvent1).ConfigureAwait(false);
				if (response.IsSuccessStatusCode)
				{
					closeApplyLeave();
					await Leavelist();
					Snackbar.Add("Saved Successfully", Severity.Success);

				}
				else
				{
					Snackbar.Add("Failed", Severity.Error);
				}
			}
			else
			{
				Snackbar.Add("Fill the Details", Severity.Warning);
			}
		}
		catch (Exception ex)
		{
			Snackbar.Add($"Exception:{ex.Message}");
		}

	}
	#endregion

	#region leave history
	//Get the leave history of particular employee
	public async Task Leavelist()
	{
		try
		{
			string apiUrl = $"api/Leave/Employeelist/{User.Id}";
			using var client = HttpClientFactory.CreateClient("API");
			var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.ListModel>>();
				SingleLeaveList = json;
				StateHasChanged();
			}
			else
			{
				Snackbar.Add("Failed");
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}
	#endregion

	#region employee totalleave based on leavetype
	//get the particular employee  totalleave  of each leavetype 
	public async Task chart()
	{
		try
		{
			string apiUrl = $"api/Leave/chartviewsick/{User.UserName}";
			using var client = HttpClientFactory.CreateClient("API");
			var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.Chartmodel>>();
				chartmodel = json;
				StateHasChanged();
			}
			else
			{
				Snackbar.Add("Failed");
			}
		}
		catch (Exception ex)
		{
			Snackbar.Add($"Exception: {ex.Message}");
		}
	}
	#endregion

	#region employee Annual leave 
	//get the particular employee Annual leave 
	private int Index = -1;
	public async Task AnnualCount()
	{
		try
		{
			string apiUrl = $"api/Dashboards/EmpLeaveDetails/{User.Id}";
			using var client = HttpClientFactory.CreateClient("API");
			var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.Dashboard>>();
				AnnualLeaveCount = json;
				Console.WriteLine("Leave Graph Done");
				StateHasChanged();
			}
			else
			{
				Console.WriteLine("Failed");
			}
		}
		catch (Exception ex)
		{
			Snackbar.Add($"Exception: {ex.Message}");
		}
	}
	#endregion

	#region edit leave
	//assigning value for Editing the leave details
	public void OpenInLineDialog(int ID)
	{

		var list = SingleLeaveList.FirstOrDefault(x => x.ID == ID);
		if (list != null)
		{
			Employeeleave.ID = list.ID;
			Employeeleave.leavetypeid = list.leavetypeid;
			Employeeleave.StartDate = list.StartDate;
			Employeeleave.EndDate = list.EndDate;
			Employeeleave.Reason = list.Reason;
		}
		value = true;
	}
	//updating the edited leave details
	public async Task EditSaves()
	{
		try
		{
			string apiUrl = $"api/Leave/Editleave/{Employeeleave.ID}";
			using var client = HttpClientFactory.CreateClient("API");
			var response = await client.PutAsJsonAsync(apiUrl, Employeeleave).ConfigureAwait(false);
			if (response.IsSuccessStatusCode)
			{
				await Leavelist();
				Snackbar.Add("Saved Successfully", Severity.Success);
				Cancel();
			}
			else
			{
				Snackbar.Add("Failed", Severity.Error);
			}
		}
		catch (Exception ex)
		{
			Snackbar.Add($"Exception: {ex.Message}");
		}
	}
	#endregion

	#region leavetype details[Dropdown]
	//leave type details for dropdown
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
				leaveTypeapprove = json;
				StateHasChanged();
			}
			else
			{
				Snackbar.Add("Saved Failed");
			}
		}
		catch (Exception ex)
		{
			Snackbar.Add($"Exception: {ex.Message}");
		}
	}
	#endregion


	public void closeApplyLeave()
	{
		value1 = false;
	}
	public void OpenDialog()
	{
		value1 = true;
	}

	public void Cancel()
	{
		value = false;
	}

	protected override async Task OnInitializedAsync()
	{
		var userJson = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "user");
		User = JsonSerializer.Deserialize<LoginDetails>(userJson);
		await LoadDataAsync();
	}

	private async Task LoadDataAsync()
	{
		var tasks = new List<Task>
		{
			 AnnualCount(),
			 Leavelist(),
			 chart(),
			 LeaveType()
		};
		await Task.WhenAll(tasks).ConfigureAwait(false);
	}
	private Func<HRMS.Model.ListModel, String> statuscolor => y =>
	{
		string style = "";

		if (y.Status == "Accepted")

			style += "color:#00ef63";

		else if (y.Status == "Pending")

			style += "color:#ffaa00";

		else if (y.Status == "Rejected")

			style += "color:#f21c0d";

		return style;
	};

	private async Task GoBack()
	{
		await JSRuntime.InvokeVoidAsync("history.back");
	}

	public void EditDialog()
	{
		value = true;

	}
}