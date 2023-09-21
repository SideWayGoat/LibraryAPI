using LibraryAPI.DTOs;
using LibraryAPI.Models;
using Newtonsoft.Json;
using System.Text;

namespace LibraryAPI.Services
{
    public class BaseService : IBaseService
    {
        public ResponseDTO responseModel { get; set; }
        public IHttpClientFactory clientFactory { get; set; }
        public BaseService(IHttpClientFactory _clientFactory)
        {
            this.clientFactory = _clientFactory;
            this.responseModel = new ResponseDTO();
        }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = clientFactory.CreateClient("Library");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                client.DefaultRequestHeaders.Clear();
                if(apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data)
                        , Encoding.UTF8, "application/json");
                }
                HttpResponseMessage apiRes = null;
                switch (apiRequest.ApiType)
                {
                    case StaticDetails.ApiType.GET:
                        message.Method = HttpMethod.Get;
                        break;
                    case StaticDetails.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case StaticDetails.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case StaticDetails.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                }
                apiRes = await client.SendAsync(message);

                var apiContent = await apiRes.Content.ReadAsStringAsync();

                var apiResponseDTO = JsonConvert.DeserializeObject<T>(apiContent);
                return apiResponseDTO;
            }
            catch (Exception e)
            {
                var dto = new ResponseDTO
                {
                    DisplayMessage = "Error",
                    ErrorMessages = new List<string> { e.Message },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var apiResponseDTO = JsonConvert.DeserializeObject<T>(res);

                return apiResponseDTO;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
