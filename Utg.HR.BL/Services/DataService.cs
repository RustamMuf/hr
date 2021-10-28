using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Utg.HR.Common.Models;
using Utg.HR.Common.Models.ClientModel;
using Utg.HR.Common.Services;

namespace Utg.HR.BL.Services
{
	public class DataService : IDataService
	{
		private static readonly HttpClient Client = new HttpClient();
		private readonly IConfiguration configuration;

		public DataService(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public async Task<IEnumerable<string>> AddToAcceasProfile(List<int> userProfilesToAdd)
		{
			var basicUser = configuration["ImportAuth:Login"];
			var basicPassword = configuration["ImportAuth:Password"];
			var errors = new List<string>();
			var byteArray = Encoding.ASCII.GetBytes($"{basicUser}:{basicPassword}");
			Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
			var url = $"{configuration["Api:Main"]}/{configuration["Api:AddToAccessProfile"]}";

			int chunkSize = 20;
			var step = 0;
			var chunk = userProfilesToAdd.Skip(step * chunkSize).Take(chunkSize);

			while (chunk.Any())
			{
				try
				{
					string paylocad = JsonConvert.SerializeObject(chunk);
					HttpContent content = new StringContent(paylocad, Encoding.UTF8, "application/json");

					var response = await Client.PostAsync(url, content);
					response.EnsureSuccessStatusCode();
				}
				catch (Exception ex)
				{
					errors.Add($"[Role] {step} {ex}");
				}
				step++;
				chunk = userProfilesToAdd.Skip(step * chunkSize).Take(chunkSize);
			}
			return errors;
		}

		public async Task<IEnumerable<UserProfileApiModel>> GetUserProfiles(IEnumerable<Guid> userProfileOuterIds, string auth = "")
		{
			var url = $"{configuration["Api:Main"]}/{configuration["Api:UserProfilesByOuterIds"]}";
			var list = userProfileOuterIds.Select(guid => guid.ToString());
			var result = await GetUserProfilesFromApi(url, list, auth);
			return result;
		}

		public async Task<IEnumerable<UserProfileApiModel>> GetUserProfiles(IEnumerable<int> userprofileIds, string auth = "")
		{
			var url = $"{configuration["Api:Main"]}/{configuration["Api:UserProfilesByIds"]}";
			var list = userprofileIds.Select(value => value.ToString());
			var result = await GetUserProfilesFromApi(url, list, auth);
			return result;
		}

		private async Task<IEnumerable<UserProfileApiModel>> GetUserProfilesFromApi(string apiUrl, IEnumerable<string> userIds, string auth)
		{
			var users = new List<UserProfileApiModel>();
			int chunkSize = 20;
			var step = 0;
			var chunk = userIds.Skip(step * chunkSize).Take(chunkSize);
			SetBasicAuthHeader();
			while (chunk.Any())
			{
				var url = $"{apiUrl}?ids={string.Join("&ids=", chunk)}";
				var response = await Client.GetAsync(url);
				response.EnsureSuccessStatusCode();
				string responseBody = await response.Content.ReadAsStringAsync();
				var usersChunk = JsonConvert.DeserializeObject<IEnumerable<UserProfileApiModel>>(responseBody);
				users.AddRange(usersChunk);
				step++;
				chunk = userIds.Skip(step * chunkSize).Take(chunkSize);
			}

			return users;
		}

		public async Task<PagedResult<UserProfileApiModel>> GetAddressBook(AdressbookRequest clientRequest, string auth = "")
		{
			var url = $"{configuration["Api:Main"]}/{configuration["Api:AddressBook"]}?";
			//url = AddQueryParameter(clientRequest.Skip, nameof(clientRequest.Skip), url);
			//url = AddQueryParameter(clientRequest.Take, nameof(clientRequest.Take), url);
			url = AddQueryParameter(clientRequest.CompanyId, nameof(clientRequest.CompanyId), url);
			url = AddQueryParameter(clientRequest.DepartmentId, nameof(clientRequest.DepartmentId), url);
			url = AddQueryParameter(clientRequest.PositionId, nameof(clientRequest.PositionId), url);
			url = AddQueryParameter(clientRequest.Search, nameof(clientRequest.Search), url);
			url = AddQueryParameter(clientRequest.SortBy, nameof(clientRequest.SortBy), url);
			url = AddQueryParameter(clientRequest.SortDirection, nameof(clientRequest.SortDirection), url);

			var responseBody = await GetDataFromApi(url, auth);
			var data = JsonConvert.DeserializeObject<PagedResult<UserProfileApiModel>>(responseBody);
			return data;
		}

		private static string AddQueryParameter(int? queryParameter, string name, string url)
		{
			if (queryParameter.HasValue)
			{
				url = $"{url}{name}={queryParameter.Value}&";
			}

			return url;
		}

		private static string AddQueryParameter(string queryParameter, string name, string url)
		{
			if (!string.IsNullOrWhiteSpace(queryParameter))
			{
				url = $"{url}{name}={queryParameter}&";
			}

			return url;
		}

		private async Task<string> GetDataFromApi(string url, string auth)
		{
			SetBasicAuthHeader();
			var response = await Client.GetAsync(url);
			//var s = await response.Content.ReadAsStringAsync();
			response.EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();
			return responseBody;
		}

		public async Task<PositionsApiModel> GetPosition(int id, string auth = "")
		{
			var url = $"{configuration["Api:Main"]}/{configuration["Api:Positions"]}/{id}";

			var responseBody = await GetDataFromApi(url, auth);
			var data = JsonConvert.DeserializeObject<PositionsApiModel>(responseBody);
			return data;
		}


		private static void SetAuthHeader(string auth)
		{
			if (!string.IsNullOrWhiteSpace(auth))
			{
				Client.DefaultRequestHeaders.Clear();
				Client.DefaultRequestHeaders.Add("Authorization", auth);
			}
		}

		private void SetBasicAuthHeader()
		{
			var basicUser = configuration["BasicAuth:Login"];
			var basicPassword = configuration["BasicAuth:Password"];
			var byteArray = Encoding.ASCII.GetBytes($"{basicUser}:{basicPassword}");
			Client.DefaultRequestHeaders.Clear();
			Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
		}

	}
}
