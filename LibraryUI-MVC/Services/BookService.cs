using LibraryAPI.DTOs;
using LibraryAPI;
using LibraryAPI.Services;
using LibraryBookModels.Models;
using LibraryAPI.Models;

namespace LibraryUI_MVC.Services
{
    public class BookService : BaseService, IBookService
    {
        private readonly IHttpClientFactory _clientFactory;

        public BookService(IHttpClientFactory _clientFactory) : base(_clientFactory)
        {
            this._clientFactory = _clientFactory;
        }

        public async Task<T> CreateBookAsync<T>(CreateBookDTO createBookDTO)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = createBookDTO,
                Url = StaticDetails.BookApiBase + "/book/create",
                AccessToken = ""
            });
        }

        public async Task<T> DeleteBookAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = StaticDetails.ApiType.DELETE,
                Url = StaticDetails.BookApiBase + "/book/delete"
            });
        }

        public async Task<T> GetAllBooks<T>()
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = StaticDetails.BookApiBase + "/book/all",
                AccessToken = ""
            });
        }

        public async Task<T> GetBookBySearch<T>(string search)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = StaticDetails.BookApiBase + $"/book/search/{search}" ,
                AccessToken = ""
            });
        }

        public async Task<T> UpdateBookAsync<T>(UpdateBookDTO model, int id)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = StaticDetails.ApiType.PUT,
                Url = StaticDetails.BookApiBase + $"/book/update/{id}",
                AccessToken = "",
                Data = model
            });
        }
    }
}

