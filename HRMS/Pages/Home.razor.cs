using HRMS.Model;
using ApexCharts;

using Microsoft.JSInterop;
using MudBlazor;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;
using static MudBlazor.CategoryTypes;
using static MudBlazor.Colors;


namespace HRMS.Pages
{

    public partial class Home
    {
        #region propertieS
      
        public bool value;

        List<HRMS.Model.CardModel> LeaveList = new List<HRMS.Model.CardModel>();
        HRMS.Model.CardModel cardlist = new HRMS.Model.CardModel();

        List<HRMS.Model.Barchart> TodayLeavelist = new List<HRMS.Model.Barchart>();

        CardModel cardobj = new CardModel();

        public bool emptycard = true;
        private bool visible;
        #endregion

        #region barchart
        //getting values for barchart 
        public async Task TodayLeaveCount()
        {
            try
            {
                const string apiUrl = "api/Leave/Barchart";
                using var client = HttpClientFactory.CreateClient("API");
                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.Barchart>>();
                    TodayLeavelist = json;
                    StateHasChanged();
                }
                else
                {
                    Console.WriteLine("Failed", Severity.Error);
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Exception: {ex.Message}");
            }
        }
        #endregion

        #region leave requests
        //assigning values for card model
        public void valuefunc(int ID)
        {
            foreach (var items in LeaveList)
            {
                var list = LeaveList.FirstOrDefault(x => x.ID == ID);
                if (list != null)
                {
                    cardlist.ID = list.ID;
                    cardlist.EmployeeCode = list.EmployeeCode;
                    cardlist.FirstName = list.FirstName;
                    cardlist.Leavetypename = list.Leavetypename;
                    cardlist.Reason = list.Reason;
                    cardlist.Detailedreason = list.Detailedreason;
                    cardlist.StartDate = list.StartDate;
                    cardlist.NoOfDays = list.NoOfDays;
                    cardlist.Designation = list.Designation;
                    cardlist.Role = list.Role;
                    cardlist.wordstartdate = list.wordstartdate;
                    cardlist.wordenddate = list.wordenddate;
                }
            }
            Dialog();
        }

        //the card model
        public async Task Leave()
        {
            try
            {
                string apiUrl = $"api/Leave/Cards/{User.UserName}";

                using var client = HttpClientFactory.CreateClient("API");

                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.CardModel>>();
                    LeaveList = json;
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


        //updating the status=reject
        public async Task updateStatus()
        {
            try
            {
                visible = false;
                cardlist.Status = "Rejected";
                string apiUrl = $"api/Leave/Updatestatus/{cardlist.ID}";
                using var client = HttpClientFactory.CreateClient("API");
                var response = await client.PutAsJsonAsync(apiUrl, cardlist).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    await Leave();
                    Snackbar.Add("Updated Successfully", Severity.Success);
                }
                else
                {
                    Snackbar.Add("Failed To Update", Severity.Error);
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Exception: {ex.Message}");
            }
        }
        //update status=accept
        public async Task updateStatusapprove()
        {
            try
            {
                visible = false;
                cardlist.Status = "Accepted";
                string apiUrl = $"api/Leave/Updatestatus/{cardlist.EmpId}";
                using var client = HttpClientFactory.CreateClient("API");
                var response = await client.PutAsJsonAsync(apiUrl, cardlist).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    await Leave();
                    Snackbar.Add("Updated Successfully", Severity.Success);
                }
                else
                {
                    Snackbar.Add("Failed To Update", Severity.Error);
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Exception: {ex.Message}");
            }
        }
        public string id;


       
        protected override async Task OnInitializedAsync()
        {
            var userJson = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "user");
            User = JsonSerializer.Deserialize<LoginDetails>(userJson);
            await TodayLeaveCount();

            await Leave();

       

        }

        private async Task GoBack()
        {
            await JSRuntime.InvokeVoidAsync("history.back");
        }


        private void Dialog() => visible = true;
        void Submit() => visible = false;

        private DialogOptions dialogOptions = new()
        {
            FullWidth = true
        };

 
    }
}