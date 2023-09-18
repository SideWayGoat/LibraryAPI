using LibraryAPI.Data;
using LibraryAPI.DTOs;
using LibraryAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace LibraryAPI.BookRepository
{
    public class BookRepo : BaseService, IBookRepo
    {
        private readonly IHttpClientFactory _clientFactory;

        public BookRepo(IHttpClientFactory _clientFactory) : base(_clientFactory)
        {
            this._clientFactory = _clientFactory;
        }

        public async Task<T> CreateBookAsync<T>(CreateBookDTO createBookDTO)
        {
            return await this.SendAsync<T>(new Models.ApiRequest
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = createBookDTO,
                Url = StaticDetails.BookApiBase + "/book/create",
                AccessToken = ""
            });
        }

        public async Task<T> DeleteBookAsync<T>(int id)
        {
            return await this.SendAsync<T>(new Models.ApiRequest
            {
                ApiType = StaticDetails.ApiType.DELETE,
                Url = StaticDetails.BookApiBase + "/book/delete"
            });
        }

        public async Task<T> GetAllBooks<T>()
        {
            return await this.SendAsync<T>(new Models.ApiRequest
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = StaticDetails.BookApiBase + "/book/all",
                AccessToken = ""
            });
        }

        public async Task<T> GetBookByTitle<T>(string title)
        {
            return await this.SendAsync<T>(new Models.ApiRequest
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = StaticDetails.BookApiBase + "/book/search/{Title}",
                AccessToken = ""
            });
        }

        public async Task<T> UpdateBookAsync<T>(UpdateBookDTO model)
        {
            return await this.SendAsync<T>(new Models.ApiRequest
            {
                ApiType = StaticDetails.ApiType.PUT,
                Url = StaticDetails.BookApiBase + "/book/update",
                AccessToken = ""
            });
        }
    }
}
