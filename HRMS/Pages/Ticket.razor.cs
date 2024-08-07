using HRMS.Model;

using Microsoft.JSInterop;
using MudBlazor;
using System.Net.Http.Json;
using System.Text.Json;
using System.Xml.Linq;

namespace HRMS.Pages
{
	public partial class Ticket
	{
		private IEnumerable<TicketModel> Elements = new List<TicketModel>();
		private string _searchString;
		private string _noResultsMessage;

		private List<string> _events = new();
		private Func<HRMS.Model.TicketModel, bool> _quickFilter => x =>
		{
			if (string.IsNullOrWhiteSpace(_searchString))
				return true;

			if ($"{x.Designation} {x.Priority} {x.Status} {x.FirstName} {x.TicketSubject}".Contains(_searchString, StringComparison.OrdinalIgnoreCase))
				return true;

			var hasResults = ticketModellist.Any(e => $"{x.Designation}{x.RaiseDate} {x.Priority} {x.Status} {x.FirstName} {x.TicketSubject}".Contains(_searchString, StringComparison.OrdinalIgnoreCase));

			return hasResults;
		};

		TicketCounts ticketCounts = new TicketCounts();
		TicketModel ticketModel = new TicketModel();
		List<TicketModel> ticketModellist = new List<TicketModel>();
		List<TicketModel> ticketHistory = new List<TicketModel>();
		TicketCounts ticketcounts = new TicketCounts();

		protected override async Task OnInitializedAsync()
		{
            var userJson = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "user");
            User = JsonSerializer.Deserialize<LoginDetails>(userJson);

            await TotalTicket();
			await TicketCount();
			await TicketHistory();
		}
		#region Ticket Raised
		public async Task TotalTicket()
		{
			try
			{
				string apiUrl = $"api/Tickets/OverALlTicketRaised/{User.Id}";
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
					Snackbar.Add("Ticket Fetch Failed");
				}
			}
			catch (Exception ex)
			{
				Snackbar.Add(ex.Message, Severity.Error);
			}
		}
		#endregion

		#region Ticket History
		public async Task TicketHistory()
		{
			try
			{
				string apiUrl = $"api/Tickets/TicketHistory/{User.Id}";
				using var client = HttpClientFactory.CreateClient("API");

				var response = await client.GetAsync(apiUrl).ConfigureAwait(false);

				if (response.IsSuccessStatusCode)
				{
					var json = await response.Content.ReadFromJsonAsync<List<TicketModel>>();
					ticketHistory = json;
					StateHasChanged();
				}
				else
				{
					Snackbar.Add("Ticket Fetch Failed");
				}

			}
			catch (Exception ex)
			{
				Snackbar.Add(ex.Message, Severity.Error);
			}
		}
		#endregion

		#region Ticket Count
		public async Task TicketCount()
		{
			try
			{
				const string apiUrl = "api/Tickets/TotalTicketCount";
				using var client = HttpClientFactory.CreateClient("API");

				var response = await client.GetAsync(apiUrl).ConfigureAwait(false);

				if (response.IsSuccessStatusCode)
				{
					var json = await response.Content.ReadFromJsonAsync<TicketCounts>();

					ticketcounts = json;
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

		#region Accept
		public async Task Approved(int ID)
		{

			var rows = ticketModellist.FirstOrDefault(x => x.ID == ID);
			if (rows != null)
			{

				string apiUrl5 = $"api/Tickets/Approve /{rows.ID}";
				using var client = HttpClientFactory.CreateClient("API");
				var response = await client.PutAsJsonAsync(apiUrl5, ticketModellist).ConfigureAwait(false);
				if (response.IsSuccessStatusCode)
				{

					Snackbar.Add($"{rows.FirstName}'s Ticket Request Approved", Severity.Success);
					StateHasChanged() ;
				}
				else
				{
					Snackbar.Add("Failed");
				}

			}
            await TotalTicket();
            await TicketCount();
        }
		#endregion

		#region Reject
		public async Task Rejected(int ID)
		{
			var rows = ticketModellist.FirstOrDefault(x => x.ID == ID);
			if (rows != null)
			{

				string apiUrl5 = $"api/Tickets/Reject /{rows.ID}";
				using var client = HttpClientFactory.CreateClient("API");
				var response = await client.PutAsJsonAsync(apiUrl5, ticketModellist).ConfigureAwait(false);
				if (response.IsSuccessStatusCode)
				{

					Snackbar.Add($"{rows.FirstName}'s Ticket Request Declined", Severity.Error);
					StateHasChanged();
				}
				else
				{
					Snackbar.Add("Failed");
				}

			}
            await TotalTicket();
            await TicketCount();
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

       

        //Back
        private async Task GoBack()
		{
			await JSRuntime.InvokeVoidAsync("history.back");
		}

	}
}


