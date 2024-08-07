using Microsoft.JSInterop;
using MudBlazor;
using System.Net.Http.Json;
using System.Net.Http;
using System.Reflection.Emit;
using HRMS.Model;
using ApexCharts;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace HRMS.Pages
{
    public partial class Attendance
    {
        AttendanceModel AttendanceModelObj = new AttendanceModel();
        AttendanceTotalPresent present = new AttendanceTotalPresent();
        private DateRange _dateRange { get; set; } = new DateRange(DateTime.Now.Date, DateTime.Now.Date);
        private string _searchString;
        private string _noResultsMessage;

        #region Filter        

        private IEnumerable<HRMS.Model.AttendanceModel> FilteredAttendance => AttendanceEvent.Where(a => a.EmployeeCode == User.UserName);
        private IEnumerable<HRMS.Model.AttendanceModel> Profile => FilteredAttendance.GroupBy(b => b.EmpId).Select(c => c.First());
        private IEnumerable<HRMS.Model.AttendanceModel> DateFilter1 => AttendanceEvent.Where(item => item.date >= _dateRange.Start && item.date <= _dateRange.End).ToList();
        private IEnumerable<HRMS.Model.AttendanceModel> DateFilter2 => FilteredAttendance.Where(item => item.date >= _dateRange.Start && item.date <= _dateRange.End).ToList();

        private Func<HRMS.Model.AttendanceModel, bool> _quickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if ($"{x.EmployeeName} {x.EmployeeCode} {x.date}".Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            var hasResults = AttendanceEvent.Any(e => $"{x.EmployeeName} {x.EmployeeCode} {x.date}".Contains(_searchString, StringComparison.OrdinalIgnoreCase));

            return hasResults;
        };
        #endregion

        #region Functions
        private async Task GoBack()
        {
            await JSRuntime.InvokeVoidAsync("history.back");
        }
        #endregion

        #region API
        List<HRMS.Model.AttendanceModel> AttendanceEvent = new List<HRMS.Model.AttendanceModel>();
        List<HRMS.Model.AttendanceTotalPresent> Present = new List<HRMS.Model.AttendanceTotalPresent>();

        protected override async Task OnInitializedAsync()
        {
            var userJson = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "user");
            User = JsonSerializer.Deserialize<LoginDetails>(userJson);

            await AttendanceView();
            await TotalPresent();
        }
        public async Task AttendanceView()
        {
            // Attendance View
            const string apiUrl = "api/Attendances/AttendanceView";
            using var client1 = HttpClientFactory.CreateClient("API");
            var response1 = await client1.GetAsync(apiUrl).ConfigureAwait(false);
            if (response1.IsSuccessStatusCode)
            {
                var json = await response1.Content.ReadFromJsonAsync<List<HRMS.Model.AttendanceModel>>();
                AttendanceEvent = json;
                StateHasChanged();
            }
            else
            {
                Snackbar.Add("Attendance View Failed");
            }
        }
        public async Task TotalPresent()
        {
            // Total Present
            const string apiUrlPresent = "api/Attendances/TotalPresent";
            using var client2 = HttpClientFactory.CreateClient("API");
            var response2 = await client2.GetAsync(apiUrlPresent).ConfigureAwait(false);
            if (response2.IsSuccessStatusCode)
            {
                var json = await response2.Content.ReadFromJsonAsync<List<HRMS.Model.AttendanceTotalPresent>>();
                Present = json;
                StateHasChanged();
            }
            else
            {
                Snackbar.Add("Total Present Failed");
            }
        }
        #endregion

        #region Attendance Update
        private bool visible;
        private void Dialog() => visible = true;
        public void update(int Id)
        {
            var list = AttendanceEvent.FirstOrDefault(x => x.Id == Id);
            if (list != null)
            {
                AttendanceModelObj.Id = list.Id;
                AttendanceModelObj.EmpId = list.EmpId;
                AttendanceModelObj.EmployeeCode = list.EmployeeCode;
                AttendanceModelObj.EmployeeName = list.EmployeeName;
                AttendanceModelObj.date = list.date;
                AttendanceModelObj.PunchIn = list.PunchIn;
                AttendanceModelObj.PunchOut = list.PunchOut;
            }
            Dialog();
        }
        void Cancel()
        {
            visible = false;
        }

        public async Task Submit(int Id)
        {
            visible = false;
            string apiUrlSubmit = $"api/Attendances/Update/{AttendanceModelObj.Id}";
            using var clientSubmit = HttpClientFactory.CreateClient("API");
            var responseSubmit = await clientSubmit.PutAsJsonAsync(apiUrlSubmit, AttendanceModelObj).ConfigureAwait(false);
            if (responseSubmit.IsSuccessStatusCode)
            {
                Snackbar.Add("Attendance Updated Successfully",Severity.Success);
            }
            else
            {
                Snackbar.Add("Attendance Update Failed",Severity.Error);
            }
            await AttendanceView();
            await TotalPresent();
        }
        #endregion

    }
}