using ApexCharts;
using HRMS.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using MudBlazor;
using MudBlazor.Charts;
using MudExtensions.Enums;
using System.Globalization;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;

namespace HRMS.Pages
{
    public partial class Dashboard
    {
        #region Properties and Variables
        
        HRMS.Model.Dashboard DashboardEvent = new HRMS.Model.Dashboard();
        HRMS.Model.Dashboard DashboardEvent1 = new HRMS.Model.Dashboard();
        List<HRMS.Model.Dashboard> DashboardList = new List<HRMS.Model.Dashboard>();
        List<HRMS.Model.Dashboard> DashboardList1 = new List<HRMS.Model.Dashboard>();
        List<HRMS.Model.Dashboard> Gender = new List<HRMS.Model.Dashboard>();
        List<HRMS.Model.Dashboard> LeaveCount = new List<HRMS.Model.Dashboard>();
        List<HRMS.Model.Dashboard> EmployeeofMonth = new List<HRMS.Model.Dashboard>();
        List<HRMS.Model.Dashboard> EmpProjectHistory = new List<HRMS.Model.Dashboard>();
        List<HRMS.Model.Project> ProjectEvents = new List<HRMS.Model.Project>();
        List<TicketModel> TicketModellist = new List<TicketModel>();
        List<AttendanceModel> AttendanceEvents = new List<AttendanceModel>();
        List<HRMS.Model.Dashboard> TodayLeave = new List<HRMS.Model.Dashboard>();
        List<HRMS.Model.Barchart> TodayLeave01 = new List<HRMS.Model.Barchart>();
        List<HRMS.Model.Project> NewProject = new List<HRMS.Model.Project>();
        bool _loading = true;
        bool visible;
        bool visible1;
        bool visible2;
        LoaderType _loaderType = LoaderType.Circular;
        private ApexChartOptions<HRMS.Model.Dashboard> options { get; set; } = new();
        private ApexChartOptions<HRMS.Model.Dashboard> options02 { get; set; } = new();

        public bool show = false;

        public DateTime ConvertTimeIn;
        public DateTime ConvertTimeOut;


        private ChartOptions donutChartOptions = new ChartOptions
        {
            ChartPalette = new[] { "#783bca", "#56aec1" }

        };


        #endregion

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
                    TicketModellist = json;
                    StateHasChanged();
                }
                else
                {
                    Console.WriteLine("Ticket Raise Failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, Severity.Error);
            }
        }
        #endregion

        #region Dashboard Count Details
        public async Task Dashboarddisplay()
        {
            try
            {
                const string apiUrl = "api/Dashboards";
                using var client = HttpClientFactory.CreateClient("API");
                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadFromJsonAsync<HRMS.Model.Dashboard>();
                    DashboardEvent = json;
                    StateHasChanged();
                }
                else
                {
                    Console.WriteLine("Count Failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region Employee of Month
        public async Task Employeeofmonth()
        {
            try
            {
                const string apiUrl = "api/Dashboards/EmployeeOfMonth";
                using var client = HttpClientFactory.CreateClient("API");
                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.Dashboard>>();
                    EmployeeofMonth = json;
                    StateHasChanged();
                }
                else
                {
                    Console.WriteLine("EOM Failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region Time Schedule Edit
        public async Task TimeSchedule()
        {
            try
            {
                const string apiUrl = "api/TimeSchedule";
                using var client = HttpClientFactory.CreateClient("API");
                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {

                    var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.Dashboard>>();
                    DashboardList = json;
                    StateHasChanged();
                }
                else
                {
                    Console.WriteLine("Time Failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Dialog() => visible = true;
        private void CloseDialog() => visible = false;

        public void OpenInLineDialog(int EventId)
        {

            var list = DashboardList.FirstOrDefault(x => x.EventId == EventId);
            if (list != null)
            {
                DashboardEvent.EventId = list.EventId;
                DashboardEvent.EventName = list.EventName;
                DashboardEvent.TimeIn = list.TimeIn;
                DashboardEvent.TimeOut = list.TimeOut;
            }

            Dialog();
        }

        public async Task Submit()
        {
            try
            {
                visible = false;
                string apiUrl = $"api/TimeSchedule/TimeUpdate/{DashboardEvent.EventId}";
                using var client = HttpClientFactory.CreateClient("API");
                var response = await client.PutAsJsonAsync(apiUrl, DashboardEvent).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    await TimeSchedule();
                    Snackbar.Add("Event Updated Successfully", Severity.Success, config =>
                    {
                        config.VisibleStateDuration = 200;
                    });
                }
                else
                {
                    Snackbar.Add("Event Update Failed", Severity.Error, config =>
                    {
                        config.VisibleStateDuration = 200;
                    });
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        #endregion

        #region Event Delete

        private void DeleteDialog() => visible2 = true;
        private void CloseDeleteDialog() => visible2 = false;
        public void OpenInLineEventDialog(int EventId)
        {

            var list = DashboardList.FirstOrDefault(x => x.EventId == EventId);
            if (list != null)
            {
                DashboardEvent.EventId = list.EventId;
                DashboardEvent.EventName = list.EventName;
                DashboardEvent.TimeIn = list.TimeIn;
                DashboardEvent.TimeOut = list.TimeOut;
            }

            DeleteDialog();
        }
        #endregion

        #region Time Schedule Add
        private void AddTimeDialog()
        {
            DashboardEvent.EventName = null;
            DashboardEvent.TimeIn = null;
            DashboardEvent.TimeOut = null;
            visible1 = true;
        }

        private void CloseTimeDialog() => visible1 = false;
        public async Task Saves()
        {
            try
            {

                var Event = DashboardList.FirstOrDefault(x => x.EventName == DashboardEvent.EventName);
                if (Event == null)
                {
                    const string apiUrl = "api/TimeSchedule";
                    using var client = HttpClientFactory.CreateClient("API");
                    var response = await client.PostAsJsonAsync(apiUrl, DashboardEvent).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        await TimeSchedule();
                        visible1 = false;
                        Snackbar.Add("Event Add Successfully", Severity.Success, config =>
                        {
                            config.VisibleStateDuration = 200;
                        });
                    }
                    else
                    {
                        Snackbar.Add("Please enter valid details", Severity.Error, config =>
                        {
                            config.VisibleStateDuration = 200;
                        });
                    }
                }
                else
                {
                    Snackbar.Add("Same Event Name/Time Not Allowed", Severity.Error, config =>
                    {
                        config.VisibleStateDuration = 200;
                    });
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task Delete()
        {
            visible2 = false;
            string apiUrl = $"api/TimeSchedule/{DashboardEvent.EventId}";
            using var client = HttpClientFactory.CreateClient("API");
            var response = await client.PutAsJsonAsync(apiUrl, DashboardEvent).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                await TimeSchedule();
                Snackbar.Add("Event Deleted Successfully", Severity.Error, config =>
                {
                    config.VisibleStateDuration = 200;
                });
            }
            else
            {
                Snackbar.Add("Failed", Severity.Error, config =>
                {
                    config.VisibleStateDuration = 200;
                });
            }

        }
        #endregion

        #region Project Details
        public async Task Project()
        {
            try
            {
                const string apiUrl = "api/Project";
                using var client = HttpClientFactory.CreateClient("API");
                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.Project>>();
                    ProjectEvents = json;
                    Console.WriteLine("Project Done");
                    StateHasChanged();
                }
                else
                {
                    Console.WriteLine("Project Failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region Gender Count
        public async Task GenderCount()
        {
            try
            {
                const string apiUrl = "api/Dashboards/GenderCount";
                using var client = HttpClientFactory.CreateClient("API");
                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.Dashboard>>();
                    Gender = json;
                    Console.WriteLine("Gender Graph Done");
                    StateHasChanged();
                }
                else
                {
                    Console.WriteLine("Failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region Employee leave Count
        public async Task EmpLeaveCount()
        {
            try
            {
                string apiUrl = $"api/Dashboards/EmpLeaveDetails/{User.Id}";
                using var client = HttpClientFactory.CreateClient("API");
                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.Dashboard>>();
                    LeaveCount = json;
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
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region Total Project Count Chart
        public async Task ProjectDetails()
        {
            try
            {
                const string apiUrl = "api/Dashboards/TotalProjectDetails";
                using var client = HttpClientFactory.CreateClient("API");
                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.Dashboard>>();
                    DashboardList1 = json;
                    Console.WriteLine("Project Graph Done");
                    StateHasChanged();
                }
                else
                {
                    Console.WriteLine("Graph Error");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region Today Present
        public async Task TotalPresent()
        {
            try
            {
                // Total Present
                string apiUrlPresent = $"api/Attendances/SearchByID/{User.Id}";
                using var client2 = HttpClientFactory.CreateClient("API");
                var response2 = await client2.GetAsync(apiUrlPresent).ConfigureAwait(false);
                if (response2.IsSuccessStatusCode)
                {
                    var json = await response2.Content.ReadFromJsonAsync<List<AttendanceModel>>();
                    AttendanceEvents = json;
                    StateHasChanged();
                }
                else
                {
                    Console.WriteLine("Total Present Failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, Severity.Error);
            }
        }

        #endregion

        #region Leave Count

        public async Task TotalLeaveCount()
        {
            try
            {
                const string apiUrl = "api/Dashboards/TodayLeaveCount";
                using var client = HttpClientFactory.CreateClient("API");
                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.Dashboard>>();
                    TodayLeave = json;
                    var selectedLeaveType = TodayLeave.FirstOrDefault(x => x.TodayLeaveCount == x.TodayLeaveCount);
                    if (selectedLeaveType.TodayLeaveCount == 0)
                    {
                        selectedLeaveType.TodayLeaveCount = 0;
                    }
                    else
                    {
                        selectedLeaveType.TodayLeaveCount = selectedLeaveType.TodayLeaveCount;
                    }
                    StateHasChanged();
                }
                else
                {
                    Console.WriteLine("Failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion

        #region OnInitializedAsync
        private async Task LoadDataAsync()
        {
            var tasks = new List<Task>
            {
                Employeeofmonth(),
                TimeSchedule(),
                GenderCount(),
                EmpLeaveCount(),
                Dashboarddisplay(),
                ProjectDetails(),
                Project(),
                EmployeeTicketRaised(),
                TotalPresent(),
                TotalLeaveCount(),
                TodayLeaveCount(),
                logindetails()
            };

            await Task.WhenAll(tasks).ConfigureAwait(false);
        }
        protected override async Task OnInitializedAsync()
        {
           /* var userJson = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "user");
            User = JsonSerializer.Deserialize<LoginDetails>(userJson);*/
			User = await LocalStore.GetItemAsync<HRMS.Model.LoginDetails>("user").ConfigureAwait(false);
			await LoadDataAsync();


            options.PlotOptions = new PlotOptions
            {
                Pie = new PlotOptionsPie
                {
                    Donut = new PlotOptionsDonut
                    {
                        Labels = new DonutLabels
                        {
                            Total = new DonutLabelTotal { Label = "Limit", FontSize = "12px", Color = "#D807B8", Formatter = @"function (w) {return w.globals.seriesTotals.reduce((a, b) => { return (a + b) }, 0)}" }
                        }
                    }
                }
            };

            options02.PlotOptions = new PlotOptions
            {
                Pie = new PlotOptionsPie
                {
                    Donut = new PlotOptionsDonut
                    {
                        Labels = new DonutLabels
                        {
                            Total = new DonutLabelTotal { FontSize = "12px", Color = "#D807B8", Formatter = @"function (w) {return w.globals.seriesTotals.reduce((a, b) => { return (a + b) }, 0)}" }

                        }
                    }
                }

            };

            options02.Fill = new Fill
            {
                //Colors = new List<string> { "#ff0000" }

            };


            _loading = false;
        }
        #endregion

        #region ApexChart Style
        private string GetColor(HRMS.Model.Dashboard order)
        {
            if (order.EmpGender == "Female")
            {
                return "#ff1ad1";
            }


            switch (order.ProjectStatus)
            {
                case "True":
                    return "#F3CA52";
                case "False":
                    return "#0A6847";

            }
            return null;
        }

        private string GetText(HRMS.Model.Dashboard Dash)
        {
            switch (Dash.ProjectStatus)
            {
                case "True":
                    return "On Progress";
                case "False":
                    return "Completed";

            }
            return null;
        }

        private Func<TicketModel, string> colorStyle => x =>
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

        private Func<TicketModel, string> colorStyle1 => x =>
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
        #endregion

        //API for Login
        public async Task logindetails()
        {
            try
            {
                string apiUrl = $"api/Project/ProjectLogin/{User.Id}";
                using var client = HttpClientFactory.CreateClient("API");

                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.Project>>();
                    NewProject = json;
                    StateHasChanged();
                }
                else
                {
                    Snackbar.Add("Failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, Severity.Error);
            }
        }

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
                    TodayLeave01 = json;
                    StateHasChanged();
                }
                else
                {
                    Console.WriteLine("Failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}   