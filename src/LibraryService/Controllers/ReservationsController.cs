using AutoMapper;
using LibraryService.Data.Interfaces;
using LibraryService.DTOs.ReservationDTOs;
using LibraryService.Entities;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.Controllers
{
    [ApiController]
    [Route("library/reservations")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationRepository _repo;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public ReservationsController(IReservationRepository repo, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _repo = repo;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReservationDto>>> GetAllReservations()
        {
            return await _repo.GetReservationsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDto>> GetReservationById(Guid id)
        {
            var reservation = await _repo.GetReservationByIdAsync(id);
            
            if (reservation == null) return NotFound();

            return reservation;
        }

        [HttpPost]
        public async Task<ActionResult<ReservationDto>> CreateReservation(CreateReservationDto createReservationDto)
        {
            var reservation = _mapper.Map<Reservation>(createReservationDto);
            
            _repo.AddReservation(reservation);

            var newReservation = _mapper.Map<ReservationDto>(reservation);

            // await _publishEndpoint.Publish(_mapper.Map<ReservationCreated>(newReservation));

            var result = await _repo.SaveChangesAsync();

            if (!result) return BadRequest("Error while creating reservation");

            return CreatedAtAction(nameof(GetReservationById), new { id = reservation.Id }, _mapper.Map<ReservationDto>(reservation));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReservationDto>> UpdateReservation(Guid id, UpdateReservationDto updateReservationDto)
        {
            var reservation = await _repo.GetReservationEntityByIdAsync(id);

            if (reservation == null) return NotFound();

            reservation.Observation = updateReservationDto.Observation ?? reservation.Observation;
            reservation.ReservationDate = updateReservationDto.ReservationDate ?? reservation.ReservationDate;

            // await _publishEndpoint.Publish(_mapper.Map<ReservationUpdated>(reservation));

            var result = await _repo.SaveChangesAsync();

            if (!result) return BadRequest("Error while updating reservation");

            return _mapper.Map<ReservationDto>(reservation);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReservation(Guid id)
        {
            var reservation = await _repo.GetReservationEntityByIdAsync(id);

            if (reservation == null) return NotFound();

            _repo.RemoveReservation(reservation);

            // await _publishEndpoint.Publish(_mapper.Map<ReservationDeleted>(reservation));

            var result = await _repo.SaveChangesAsync();

            if (!result) return BadRequest("Error while deleting reservation");

            return NoContent();
        }
    }
}
