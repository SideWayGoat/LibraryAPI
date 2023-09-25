using AutoMapper;
using LibraryAPI.DTOs;
using LibraryBookModels.Models;
using LibraryUI_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace LibraryUI_MVC.Controllers
{
	public class BookController : Controller
	{
		private readonly IBookService _bookService;
		public BookController(IBookService bookService)
		{
			this._bookService = bookService;
		}
		public async Task<IActionResult> BookIndex(string searchString)
		{
			List<BookDTO> books = new List<BookDTO>();
			var response = await _bookService.GetAllBooks<ResponseDTO>();
			if (!String.IsNullOrEmpty(searchString))
			{
				response = await _bookService.GetBookBySearch<ResponseDTO>(searchString);
			}
			if (response != null && response.IsSuccess)
			{
				books = JsonConvert.DeserializeObject<List<BookDTO>>(Convert.ToString(response.Result));
			}
			return View(books);
		}

		public async Task<IActionResult> CreateBook()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateBook(CreateBookDTO model)
		{
			if (ModelState.IsValid)
			{
				var response = await _bookService.CreateBookAsync<ResponseDTO>(model);
				if (response != null && response.IsSuccess)
				{
					return RedirectToAction(nameof(BookIndex));
				}
			}
			return View(model);
		}

		public async Task<IActionResult> Details(string Title)
		{
			List<BookDTO> bDTO = new List<BookDTO>();
			BookDTO book = new BookDTO();
			var response = await _bookService.GetBookBySearch<ResponseDTO>(Title);
			if (response != null && response.IsSuccess)
			{
				bDTO = JsonConvert.DeserializeObject<List<BookDTO>>(Convert.ToString(response.Result));
				book = bDTO[0];
				return View(book);
			}
			return NotFound();
		}

		public async Task<IActionResult> UpdateBook(string title)
		{
			var response = await _bookService.GetBookBySearch<ResponseDTO>(title);
			if (response != null && response.IsSuccess)
			{

				List<UpdateBookDTO> model = JsonConvert.DeserializeObject<List<UpdateBookDTO>>(Convert.ToString(response.Result));
				var book = model[0];
				return View(book);
			}
			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> UpdateBook(UpdateBookDTO model, int id, string action)
		{
			if (ModelState.IsValid)
			{
				id = model.id;
				switch (action)
				{
					case "Delete":
						var response = await _bookService.DeleteBookAsync<ResponseDTO>(id);
						if(response != null && response.IsSuccess)
						{
							return RedirectToAction(nameof(BookIndex));
						}
						break;
					case "Save":
						response = await _bookService.UpdateBookAsync<ResponseDTO>(model, id);
						if (response != null && response.IsSuccess)
						{
							return RedirectToAction(nameof(BookIndex));
						}
						break;
				}

			}
			return View(model);
		}

		//public async Task<IActionResult> Search(string searchString)
		//{
		//	List<BookDTO> books = new List<BookDTO>();
		//	var response = await _bookService.GetBookBySearch<ResponseDTO>(searchString);
		//	if (response != null && response.IsSuccess)
		//	{
		//		books = JsonConvert.DeserializeObject<List<BookDTO>>(Convert.ToString(response.Result));
		//		return View(books);
		//	}
		//	return NotFound();
		//}
	}
}
