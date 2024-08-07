using Microsoft.JSInterop;
using MudBlazor;
using System.Net.Http.Json;
using System.Net.Http;
using HRMS.Model;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data;

namespace HRMS.Pages
{
    public partial class Holidays
    {
        #region properties and variables
        List<HRMS.Model.Holiday> holidayEvents = new List<HRMS.Model.Holiday>();

        HRMS.Model.Holiday _holiday = new HRMS.Model.Holiday();
        private string _searchString1 = string.Empty;
        private string _searchString = string.Empty;
        private bool visible;
        private bool visible1;
        private bool visible2;
        #endregion

        #region Goback
        private async Task GoBack()
        {
            await JSRuntime.InvokeVoidAsync("history.back");
        }
        #endregion

        #region Quickfillter

        private Func<HRMS.Model.Holiday, bool> _quickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if ($"{x.HolidayName} {x.HolidayNewDate} {x.HolidayDay} ".Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            var hasResults = holidayEvents.Any(e => $"{x.HolidayName} {x.HolidayNewDate} {x.HolidayDay} ".Contains(_searchString, StringComparison.OrdinalIgnoreCase));
            var hasResults1 = holidayEvents.Any(e => $"{e.HolidayName} {e.HolidayNewDate} {e.HolidayDay}"
             .IndexOf(_searchString, StringComparison.OrdinalIgnoreCase) >= 0);
            if (hasResults != hasResults1)
            {
                _searchString1 = "No Results found";
            }

            return hasResults;
        };

        #endregion

        #region Delete Holiday

        public void OpenInLineDeleteDialog(int Id)
        {

            var list = holidayEvents.FirstOrDefault(x => x.Id == Id);
            if (list != null)
            {
                _holiday.Id = list.Id;
                _holiday.HolidayName = list.HolidayName;
                _holiday.HolidayNewDate = list.HolidayNewDate;
                _holiday.HolidayDay = list.HolidayDay;
            }

            OpenDeleteDialog();
        }
        private void OpenDeleteDialog()
        {
            visible2 = true;
        }

        private void CloseDeleteDialog()
        {
            visible2 = false;
        }

        public async Task Delete()
        {
            try
            {
                visible2 = false;
                string apiUrl = $"api/Holidays/Delete/{_holiday.Id}";
                using var client = HttpClientFactory.CreateClient("API");
                var response = await client.PutAsJsonAsync(apiUrl, _holiday).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    await HolidayDetailsList();
                    Snackbar.Add("Holiday Deleted Successfully", Severity.Success, config =>
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, Severity.Error);
            }
        }

        #endregion

        #region HolidayList
        public async Task HolidayDetailsList()
        {
            try
            {
                const string apiUrl = "api/Holidays";
                using var client = HttpClientFactory.CreateClient("API");

                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.Holiday>>();
                    holidayEvents = json;
                    StateHasChanged();
                }
                else
                {
                    Snackbar.Add("Failed", Severity.Error, config =>
                    {
                        config.VisibleStateDuration = 200;
                    });
                }
                for (int i = 0; i < holidayEvents.Count; i++)
                {
                    holidayEvents[i].Sno = i + 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, Severity.Error);
            }
        }
        #endregion

        #region Dialog Function
        private void OpenDialog()
        {
            _holiday.HolidayName = null;
            _holiday.HolidayNewDate = null;
            visible1 = true;
        }

        private void CloseDialog()
        {
            visible1 = false;
        }

        public void OpenInLineDialog(int Id)
        {

            var list = holidayEvents.FirstOrDefault(x => x.Id == Id);
            if (list != null)
            {
                _holiday.Id = list.Id;
                _holiday.HolidayName = list.HolidayName;
                _holiday.HolidayNewDate = list.HolidayNewDate;
                _holiday.HolidayDay = list.HolidayDay;
            }

            Dialog();
        }

        private void Dialog() => visible = true;
        private void Close() => visible = false;
        #endregion

        #region Update Holiday
        public async Task Submit()
        {

            try
            {

                var holidaycheck = holidayEvents.FirstOrDefault(x => x.HolidayName == _holiday.HolidayName);

                if (holidaycheck == null)
                {

                    string apiUrl = $"api/Holidays/{_holiday.Id}";
                    using var client = HttpClientFactory.CreateClient("API");
                    var response = await client.PutAsJsonAsync(apiUrl, _holiday).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        await HolidayDetailsList();
                        visible = false;
                        Snackbar.Add("Holiday Changed Successfully", Severity.Success, config =>
                        {
                            config.VisibleStateDuration = 200;
                        });
                    }
                    else
                    {
                        Snackbar.Add("Holiday Changed Failed", Severity.Error, config =>
                        {
                            config.VisibleStateDuration = 200;
                        });
                    }
                }
                else
                {
                    Snackbar.Add("Same name are not allowed", Severity.Error, config =>
                    {
                        config.VisibleStateDuration = 200;
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, Severity.Error);
            }

        }
        #endregion

        #region Add Holiday

        public async Task Saves()
        {
            try
            {
                visible1 = false;
                var list = holidayEvents.FirstOrDefault(x => x.HolidayName == _holiday.HolidayName);
                if (list != null)
                {
                    Snackbar.Add("Same Holiday Name/Date Not Allowed", Severity.Error, config =>
                    {
                        config.VisibleStateDuration = 200;
                    });
                }
                else if (list == null)
                {
                    const string apiUrl = "api/Holidays";
                    using var client = HttpClientFactory.CreateClient("API");
                    var response = await client.PostAsJsonAsync(apiUrl, _holiday).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        await HolidayDetailsList();
                        Snackbar.Add("Holiday Add Successfully", Severity.Success, config =>
                        {
                            config.VisibleStateDuration = 200;
                        });

                    }
                    else
                    {
                        Snackbar.Add("Holiday Add Failed", Severity.Error, config =>
                        {
                            config.VisibleStateDuration = 200;
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion

        #region OnInitializedAsync
        protected override async Task OnInitializedAsync()
        {

            await HolidayDetailsList();
            var userJson = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "user");
            User = JsonSerializer.Deserialize<LoginDetails>(userJson);
        }
        #endregion

    }
}