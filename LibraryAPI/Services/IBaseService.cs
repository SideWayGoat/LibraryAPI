using LibraryAPI.DTOs;
using LibraryAPI.Models;

namespace LibraryAPI.Services
{
    public interface IBaseService : IDisposable
    {
        ResponseDTO responseModel { get; set; }
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
