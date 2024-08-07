using HRMS.Model;
using Microsoft.JSInterop;
using MudBlazor;
using System.Net.Http.Json;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Forms;


namespace HRMS.Pages
{
	public partial class ViewTicket
	{

        private bool visible;
        private int rating;
        bool success;
        string[] errors = { };
        MudForm form;
        //File save
        private IBrowserFile TicketImage { get; set; }
        private void OpenDialog()
        {
            visible = true;
            ticketModel.EmployeeCode = User.UserName;
            ticketModel.FirstName = User.FirstName;
            ticketModel.Designation = User.Role;
            ticketModel.TicketSubject = null;
            ticketModel.Priority = null;
            ticketModel.Description = null;
            ticketModel.FileContent3 = null;
        } 
       // void Submit() => visible = false;
        private DialogOptions dialogOptions = new() { FullWidth = true };



        TicketModel ticketModel = new TicketModel();
		List<TicketModel> ticketModellist = new List<TicketModel>();
        List<HRMS.Model.RolenDesignation> rolenDesignations = new List<HRMS.Model.RolenDesignation>();
        List<HRMS.Model.RolenDesignation> Designations = new List<HRMS.Model.RolenDesignation>();
        List<HRMS.Model.TicketSubjectList> ticketSubjectList = new List<HRMS.Model.TicketSubjectList>();
        List<HRMS.Model.EmployeeList> employeeDetails = new List<HRMS.Model.EmployeeList>();
        List<HRMS.Model.TicketPriority> selectPriority = new List<HRMS.Model.TicketPriority>();



        protected override async Task OnInitializedAsync()
		{
            var userJson = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "user");
            User = JsonSerializer.Deserialize<LoginDetails>(userJson);
            //await LoadUserFromLocalStorage();
            await EmployeeTicketRaised();
			await RolesAndDesignation();
            await SelectTicketSubject();
            await EmployeeDetails();
            await Priority();
        }
        #region Ticket Raised API
        public async Task EmployeeTicketRaised()
		{
			try
			{
				string apiUrl = $"api/Tickets/PersonalTicketRaised/{User.Id}";
				using var client = HttpClientFactory.CreateClient("API");

				var response = await client.GetAsync(apiUrl).ConfigureAwait(false);

				if (response.IsSuccessStatusCode)
				{
					var json = await response.Content.ReadFromJsonAsync<List<TicketModel>>();
                    ticketModellist = json;
					StateHasChanged();
				}
				else
				{
					Snackbar.Add("No Ticket Raised",Severity.Info);
				}
               
            }
			catch (Exception ex)
			{
				Snackbar.Add(ex.Message, Severity.Error);
			}
		}
        #endregion

        #region Select Ticket Subject API
        public async Task SelectTicketSubject()
		{
			try 
			{
                string apiUrl = "api/Tickets/TicketSubject";
                using var client = HttpClientFactory.CreateClient("API");

                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.TicketSubjectList>>();
                    ticketSubjectList = json;
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
        #endregion

        #region Select Priority
        public async Task Priority()
        {
            try
            {
                string apiUrl = "api/Tickets/Priority";
                using var client = HttpClientFactory.CreateClient("API");

                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.TicketPriority>>();
                    selectPriority = json;
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
        #endregion

        #region Select Employee Details
        public async Task EmployeeDetails()
        {
            try
            {
                string apiUrl = "api/EmployeeDetailsTable/Employeeoveralldetails/1";
                using var client = HttpClientFactory.CreateClient("API");

                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.EmployeeList>>();
                    employeeDetails = json;
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
        #endregion

        #region Select Role and Designation API
        public async Task RolesAndDesignation()
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
        #endregion

        #region save 
        public async Task Saves()
		{
			try
			{
				var selectedTicketSubject = ticketSubjectList.FirstOrDefault(x => x.Subject == ticketModel.TicketSubject);

              

                ticketModel.EmpId = User.Id;
                ticketModel.FirstName = User.FirstName;
                ticketModel.Designation = User.Role;
                ticketModel.TicketSubjectId = selectedTicketSubject.ID;
                ticketModel.PriorityId = selectedTicketSubject.PriorityId;
                ticketModel.CreatedBy = User.Id;



                const string apiUrl = "api/Tickets";
				using var client = HttpClientFactory.CreateClient("API");
				var response = await client.PostAsJsonAsync(apiUrl, ticketModel).ConfigureAwait(false);
				if (response.IsSuccessStatusCode)
				{
					Snackbar.Add("Ticket Raised Successfully",Severity.Success);
				}
				else
				{
					Snackbar.Add("Failed to Submit",Severity.Error);
				}
                await EmployeeTicketRaised();
            }
			catch (Exception ex)
			{
				Snackbar.Add(ex.Message);
			}
            NavigationManager.NavigateTo("/viewticket");
		}

        #endregion

        private Func<TicketModel, string> colorstyle => x =>
        {
            string style = "";

            if (x.Priority == "High")
                style += "color:#f21c0d";

            else if (x.Priority == "Medium")
                style += "color:#ffaa00";

            else if (x.Priority == "Low")
                style += "color:#00ef63";

            return style;
        };
        private Func<TicketModel, string> colorstyle1 => x =>
        {
            string style = "";

            if (x.Status == "Declined")
                style += "color:#f21c0d";

            else if (x.Status == "Pending")
                style += "color:#ffaa00";

            else if (x.Status == "Approved")
                style += "color:#00ef63";

            return style;
        };

        private async Task GoBack()
		{
			await JSRuntime.InvokeVoidAsync("history.back");
		}
      
    }
}
