using AutoMapper;
using Contracts;
using LibraryService.DTOs.BookDTOs;
using LibraryService.DTOs.CategoryDTOs;
using LibraryService.DTOs.ImageDTOs;
using LibraryService.DTOs.LoanDTOs;
using LibraryService.DTOs.ReservationDTOs;
using LibraryService.DTOs.UserDTOs;
using LibraryService.Entities;

namespace LibraryService.RequestHelpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // Book Mappings
        CreateMap<Book, BookDto>()
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
            .ForMember(dest => dest.Loans, opt => opt.MapFrom(src => src.Loans))
            .ForMember(dest => dest.Reservations, opt => opt.MapFrom(src => src.Reservations));
        CreateMap<BookDto, Book>()
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
            .ForMember(dest => dest.Loans, opt => opt.MapFrom(src => src.Loans))
            .ForMember(dest => dest.Reservations, opt => opt.MapFrom(src => src.Reservations));
        CreateMap<CreateBookDto, Book>()
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));
        CreateMap<BookDto, BookCreated>()
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(i => i.ImageUrl).ToList()))
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories.Select(c => c.Name).ToList()));
        CreateMap<Book, BookUpdated>()
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(i => i.ImageUrl).ToList()))
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories.Select(c => c.Name).ToList()));

        // Category Mappings
        CreateMap<Category, CategoryDto>()
            .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));
        CreateMap<CategoryDto, Category>()
            .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));
        CreateMap<CreateCategoryDto, Category>();

        // Reservation Mappings
        CreateMap<Reservation, ReservationDto>()
            .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));
        CreateMap<CreateReservationDto, Reservation>()
            .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

        // User Mappings
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Loans, opt => opt.MapFrom(src => src.Loans))
            .ForMember(dest => dest.Reservations, opt => opt.MapFrom(src => src.Reservations));
        CreateMap<CreateUserDto, User>();

        // Loan Mappings
        CreateMap<Loan, LoanDto>()
            .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));
        CreateMap<CreateLoanDto, Loan>();

        // Image mappings
        CreateMap<Image, ImageDto>()
            .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book));
        CreateMap<ImageDto, Image>()
            .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book));
        CreateMap<CreateImageDto, Image>()
            .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book));
    }
}
