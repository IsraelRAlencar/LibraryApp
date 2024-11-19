using LibraryService.DTOs.ReservationDTOs;
using LibraryService.Entities;

namespace LibraryService.Data.Interfaces;

public interface IReservationRepository
{
    Task<List<ReservationDto>> GetReservationsAsync();
    Task<ReservationDto> GetReservationByIdAsync(Guid id);
    Task<Reservation> GetReservationEntityByIdAsync(Guid id);
    void AddReservation(Reservation Reservation);
    void RemoveReservation(Reservation Reservation);
    Task<bool> SaveChangesAsync();
}
