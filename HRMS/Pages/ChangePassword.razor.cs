using HRMS.Model;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text.RegularExpressions;
using Microsoft.JSInterop;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HRMS.Pages
{
	public partial class ChangePassword
	{
		#region Properties and Variables
		bool isShow;
		bool isShow1;
		bool isShow2;
		public string pass;
		public string pass1;
		public string error = "";
		HRMS.Model.LoginDetails logins = new HRMS.Model.LoginDetails();
		List<HRMS.Model.LoginDetails> LoginEvent = new List<HRMS.Model.LoginDetails>();
		[CascadingParameter] MudDialogInstance MudDialog { get; set; }
		string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
		InputType PasswordInput = InputType.Password;
		MudTextField<string> pwField1;
		string PasswordInputIcon1 = Icons.Material.Filled.VisibilityOff;
		InputType PasswordInput1 = InputType.Password;
		string PasswordInputIcon2 = Icons.Material.Filled.VisibilityOff;
		InputType PasswordInput2 = InputType.Password;
		#endregion

		#region OnInitializedAsync
		protected override async Task OnInitializedAsync()
		{
			var userJson = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "user");
			User = JsonSerializer.Deserialize<LoginDetails>(userJson);
		}
		#endregion

		#region Check Password
		public async Task Newuser()
		{
			var userpass = LoginEvent.FirstOrDefault(x => x.Password == logins.Password);
			//await LoginDetails();
			if (userpass != null)
			{
				if (logins.Password == userpass.Password && userpass.Password != logins.NewPassword && pass == logins.NewPassword)
				{
					await Change();
					MudDialog.Close(DialogResult.Ok(true));
				}

				else
				{
					error = "";
					Snackbar.Add("Old Password and New Password should not be same", Severity.Error, config =>
					{
						config.VisibleStateDuration = 200;
					});
				}
			}
			else if (userpass == null)
			{
				error = "Please Enter Correct Password";
			}

		}
		#endregion

		#region Get Old Password
		public async Task LoginDetails()
		{
			try
			{
				string apiUrl = $"api/LoginDetails?user={User.UserName}&password={logins.Password}";
				using var client = HttpClientFactory.CreateClient("API");

				var response = await client.GetAsync(apiUrl).ConfigureAwait(false);

				if (response.IsSuccessStatusCode)
				{
					var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.LoginDetails>>();
					LoginEvent = json;
					await Newuser();
					//error = "Password Not Correct";
					StateHasChanged();

				}
				else
				{
					error = "Please Enter Correct Password";
					Snackbar.Add("Password Not Correct", Severity.Error, config =>
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

		#region Change Password
		public async Task Change()
		{
			try
			{
				logins.Id = User.Id;
				string apiUrl = $"api/LoginDetails/Changepassword";
				using var client = HttpClientFactory.CreateClient("API");
				var response = await client.PutAsJsonAsync(apiUrl, logins).ConfigureAwait(false);
				if (response.IsSuccessStatusCode)
				{
					Snackbar.Add("Password Changed Succesfully", Severity.Success, config =>
					{
						config.VisibleStateDuration = 200;
					});
					NavigationManager.NavigateTo("/");
					await LocalStore.ClearAsync();
					MudDialog.Close(DialogResult.Ok(true));
				}
				else
				{
					Snackbar.Add("Password Change Failed", Severity.Error, config =>
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

		#region Dialog
		void Cancel() => MudDialog.Cancel();
		#endregion

		#region Password Visiblity
		void ButtonTestclick()
		{
			if (isShow)
			{
				isShow = false;
				PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
				PasswordInput = InputType.Password;
			}
			else
			{
				isShow = true;
				PasswordInputIcon = Icons.Material.Filled.Visibility;
				PasswordInput = InputType.Text;
			}
		}


		void ButtonTestclick1()
		{
			if (isShow1)
			{
				isShow1 = false;
				PasswordInputIcon1 = Icons.Material.Filled.VisibilityOff;
				PasswordInput1 = InputType.Password;
			}
			else
			{
				isShow1 = true;
				PasswordInputIcon1 = Icons.Material.Filled.Visibility;
				PasswordInput1 = InputType.Text;
			}
		}


		void ButtonTestclick2()
		{
			if (isShow2)
			{
				isShow2 = false;
				PasswordInputIcon2 = Icons.Material.Filled.VisibilityOff;
				PasswordInput2 = InputType.Password;
			}
			else
			{
				isShow2 = true;
				PasswordInputIcon2 = Icons.Material.Filled.Visibility;
				PasswordInput2 = InputType.Text;
			}
		}

		#endregion

		#region Check Two Password
		private IEnumerable<string> PasswordStrength1(string ForgetPassword)
		{
			if (string.IsNullOrWhiteSpace(ForgetPassword))
			{
				yield return "Password is required!";
				yield break;
			}
			if (ForgetPassword.Length < 8)
				yield return "Password must be at least of length 8";
			if (!Regex.IsMatch(ForgetPassword, @"[A-Z]"))
				yield return "Password must contain at least one capital letter";
			if (!Regex.IsMatch(ForgetPassword, @"[a-z]"))
				yield return "Password must contain at least one lowercase letter";
			if (!Regex.IsMatch(ForgetPassword, @"[0-9]"))
				yield return "Password must contain at least one digit";
			if (!Regex.IsMatch(ForgetPassword, @"[!@#$%^&*()]"))
				yield return "Password must contain at least one Special Character";
		}

		private string PasswordMatch(string arg)
		{
			if (pwField1.Value != arg)
				return "Passwords don't match";
			return null;
		}
		#endregion

	}
}