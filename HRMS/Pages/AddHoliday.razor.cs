using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using HRMS.Model;
using System.Net.Http.Json;
using System.Net.Http;

namespace HRMS.Pages
{
    public partial class AddHoliday
    {


        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        void Cancel() => MudDialog.Close(DialogResult.Ok(true));

		List<HRMS.Model.Holiday> HolidayList = new List<HRMS.Model.Holiday>();
		HRMS.Model.Holiday _holidayevent { get; set; } = new HRMS.Model.Holiday();

		protected override async Task OnInitializedAsync()
		{

			const string apiUrl = "api/Holidays";
			using var client = HttpClientFactory.CreateClient("API");

			var response = await client.GetAsync(apiUrl).ConfigureAwait(false);

			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.Holiday>>();
				HolidayList = json;
				StateHasChanged();
			}
			else
			{
				Snackbar.Add("Failed");
			}
		
		}

		public async Task Saves()
        {
			try
            {
				
					const string apiUrl = "api/Holidays";
					using var client = HttpClientFactory.CreateClient("API");
					var response = await client.PostAsJsonAsync(apiUrl, _holidayevent).ConfigureAwait(false);
					if (response.IsSuccessStatusCode)
					{
						Snackbar.Add("Holiday Add Successfully");
						MudDialog.Close(DialogResult.Ok(true));
					}
					else
					{
						Snackbar.Add("Failed");
					}
				
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message);
            }
        }

    }
}