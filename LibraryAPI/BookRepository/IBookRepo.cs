using LibraryAPI.DTOs;
using System.Collections;

namespace LibraryAPI.BookRepository
{
    public interface IBookRepo
    {
        Task<T> GetAllBooks<T>();
        Task<T> GetBookByTitle<T>(string title);
        Task<T> CreateBookAsync<T>(CreateBookDTO createBookDTO);
        Task<T> UpdateBookAsync<T>(UpdateBookDTO model);
        Task<T> DeleteBookAsync<T>(int id);
    }
}
