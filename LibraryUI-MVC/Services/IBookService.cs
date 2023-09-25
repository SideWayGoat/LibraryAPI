using LibraryAPI.DTOs;

namespace LibraryUI_MVC.Services
{
    public interface IBookService
    {
        Task<T> GetAllBooks<T>();
        Task<T> GetBookBySearch<T>(string search);
        Task<T> CreateBookAsync<T>(CreateBookDTO createBookDTO);
        Task<T> UpdateBookAsync<T>(UpdateBookDTO model, int id);
        Task<T> DeleteBookAsync<T>(int id);
    }
}
