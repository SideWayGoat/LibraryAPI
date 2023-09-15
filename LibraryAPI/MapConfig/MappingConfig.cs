using AutoMapper;
using LibraryAPI.DTOs;
using LibraryBookModels;

namespace LibraryAPI.MapConfig
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Book, CreateBookDTO>().ReverseMap();
            CreateMap<Book, UpdateBookDTO>().ReverseMap();
            CreateMap<Book, BookDTO>().ReverseMap();
        }
    }
}
