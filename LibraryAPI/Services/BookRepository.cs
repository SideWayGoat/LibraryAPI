using LibraryAPI.Data;
using LibraryBookModels.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services
{
	public class BookRepository : IBookRepository
	{
		private readonly LibraryDbContext _context;
		public BookRepository(LibraryDbContext context)
		{
			this._context = context;
		}
		public async Task<Book> CreateBook(Book createBook)
		{
			var found = await _context.Books.FirstOrDefaultAsync(b => (b.Title == createBook.Title && b.Author == createBook.Author));
			if (found == null)
			{
				var result = await _context.Books.AddAsync(createBook);
				await _context.SaveChangesAsync();
				return result.Entity;
			}
			return null;
		}

		public async Task<Book> DeleteBook(int id)
		{
			var result = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
			if (result != null)
			{
				_context.Books.Remove(result);
				await _context.SaveChangesAsync();
				return result;
			}
			return null;
		}

		public async Task<IEnumerable<Book>> GetAllBooks()
		{
			return await _context.Books.ToListAsync();
		}


		public async Task<List<Book>> GetBySearch(string search)
		{
			var book = await _context.Books.Where(s =>
				(s.Title.Contains(search) ||
				s.Author.Contains(search) ||
				s.Genre.Contains(search)))
				.ToListAsync();
			return book;
		}

		public async Task<Book> UpdateBook(Book updateBook)
		{
			var result = await _context.Books.FirstOrDefaultAsync(b => b.Id == updateBook.Id);
			if (result != null)
			{
				result.Title = updateBook.Title;
				result.Author = updateBook.Author;
				result.Genre = updateBook.Genre;
				result.PublishingYear = updateBook.PublishingYear;
				result.NumberInStock = updateBook.NumberInStock;
				result.Description = updateBook.Description;

				await _context.SaveChangesAsync();
				return result;
			}
			return null;
		}
	}
}
