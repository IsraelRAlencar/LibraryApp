using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryService.Data.Interfaces;
using LibraryService.DTOs.ReservationDTOs;
using LibraryService.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Data.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly LibraryDbContext _context;
    private readonly IMapper _mapper;

    public ReservationRepository(LibraryDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void AddReservation(Reservation Reservation)
    {
        _context.Reservations.Add(Reservation);
    }

    public Task<Book> GetBookEntityByIdAsync(Guid id)
    {
        return _context.Books.FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<ReservationDto> GetReservationByIdAsync(Guid id)
    {
        return await _context.Reservations.ProjectTo<ReservationDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Reservation> GetReservationEntityByIdAsync(Guid id)
    {
        return await _context.Reservations.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<List<ReservationDto>> GetReservationsAsync()
    {
        return await _context.Reservations.ProjectTo<ReservationDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<User> GetUserEntityByIdAsync(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public void RemoveReservation(Reservation Reservation)
    {
        _context.Reservations.Remove(Reservation);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
