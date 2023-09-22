using LibraryAPI.DTOs;
using LibraryUI_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LibraryUI_MVC.Controllers
{
	public class BookController : Controller
	{
		private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            this._bookService = bookService;
        }
        public async Task<IActionResult> Index()
		{
			List<BookDTO> books = new List<BookDTO>();
			var response = await _bookService.GetAllBooks<ResponseDTO>();
			if(response != null && response.IsSuccess)
			{
				books = JsonConvert.DeserializeObject<List<BookDTO>>(Convert.ToString(response.Result));
			}
			return View(books);
		}

		public async Task<IActionResult> Search(string searchString)
		{
			ViewData["CurrentFilter"] = searchString;
			var response = await _bookService.GetBookBySearch<ResponseDTO>(searchString);
			if(response != null && response.IsSuccess)
			{
				List<BookDTO> bookDTO = JsonConvert.DeserializeObject<List<BookDTO>>(Convert.ToString(response.Result));
				return View (bookDTO);
			}
			return NotFound();
		}
	}
}
