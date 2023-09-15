using LibraryAPI.DTOs;
using System.Collections;

namespace LibraryAPI.BookRepository
{
    public interface IBookRepo
    {
        Task<IEnumerable> GetAllBooks<T>();
        Task<T> GetBookById<T>(int id);
        Task<T> CreateBookAsync<T>(CreateBookDTO createBookDTO);
        Task<T> UpdateBookAsync<T>(UpdateBookDTO model);
        Task<T> DeleteBookAsync<T>(int id);
    }
}
