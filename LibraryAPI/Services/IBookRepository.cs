using LibraryBookModels.Models;

namespace LibraryAPI.Services
{
	public interface IBookRepository
	{
		Task<IEnumerable<Book>> GetAllBooks();
		Task<List<Book>> GetBySearch(string search);
		Task<Book> CreateBook(Book createBook);
		Task<Book> UpdateBook(Book updateBook);
		Task<Book> DeleteBook(int id);
	}
}
