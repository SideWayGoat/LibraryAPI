using static LibraryAPI.StaticDetails;

namespace LibraryAPI.Models
{
    public class ApiRequest
    {
        public ApiType ApiType { get; set; }
        public string Url { get; set; }
        public Object Data { get; set; }
        public string AccessToken { get; set; }
    }
}
