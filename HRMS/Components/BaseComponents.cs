using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using HRMS.Model;
using System.Text.Json;

namespace HRMS.Components
{
    public class BaseComponents : ComponentBase
    {

        #region Dependencies


        [Inject]
        protected ILocalStorageService LocalStore { get; set; }

        #endregion Dependencies
        #region Properties

        protected HRMS.Model.LoginDetails User { get; set; } = new HRMS.Model.LoginDetails();


		#endregion Properties

		[Inject]
        public NavigationManager NavigationManager { get; set; }

		[Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public IHttpClientFactory HttpClientFactory { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        Action<SnackbarOptions> snackbarOptions = options =>
        {
            options.RequireInteraction = true;
            options.Action = "Dismiss";
            options.ActionColor = Color.Primary;
            options.VisibleStateDuration = 5000; 
        };

        protected override async Task OnInitializedAsync()
        {
            if (LocalStore != null)
            {
				User = await LocalStore.GetItemAsync<HRMS.Model.LoginDetails>("user").ConfigureAwait(false);

            }
            await base.OnInitializedAsync();
		}



    }
}