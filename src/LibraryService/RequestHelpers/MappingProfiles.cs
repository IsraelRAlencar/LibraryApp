using AutoMapper;
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
        CreateMap<CreateBookDto, Book>()
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));

        // Category Mappings
        CreateMap<Category, CategoryDto>()
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
        CreateMap<Image, ImageDto>();
        CreateMap<CreateImageDto, Image>();
    }
}
