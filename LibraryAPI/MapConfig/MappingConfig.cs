using AutoMapper;
using LibraryAPI.DTOs;
using LibraryAPI.Models;
using LibraryBookModels.Models;

namespace LibraryAPI.MapConfig
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Book, CreateBookDTO>().ReverseMap();
            CreateMap<Book, UpdateBookDTO>().ReverseMap();
            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<BookDTO, Book>().ForAllMembers(b => b.MapFrom(d => d));
        }
    }
}
