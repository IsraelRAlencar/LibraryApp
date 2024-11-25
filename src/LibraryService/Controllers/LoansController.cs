using AutoMapper;
using LibraryService.Data.Interfaces;
using LibraryService.DTOs.LoanDTOs;
using LibraryService.Entities;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.Controllers
{
    [ApiController]
    [Route("library/loans")]
    public class LoansController : ControllerBase
    {
        private readonly ILoanRepository _repo;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public LoansController(ILoanRepository repo, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _repo = repo;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<ActionResult<List<LoanDto>>> GetAllLoans()
        {
            return await _repo.GetLoansAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LoanDto>> GetLoanById(Guid id)
        {
            var loan = await _repo.GetLoanByIdAsync(id);
            
            if (loan == null) return NotFound();

            return loan;
        }

        [HttpPost]
        public async Task<ActionResult<LoanDto>> CreateLoan(CreateLoanDto createLoanDto)
        {
            var loan = _mapper.Map<Loan>(createLoanDto);

            if (loan.Book.Id != Guid.Empty)
            {
                var book = await _repo.GetBookEntityByIdAsync(loan.Book.Id);
                if (book != null) loan.Book = book;
                else return BadRequest("Book not found");
            }

            if (loan.User.Id != Guid.Empty)
            {
                var user = await _repo.GetUserEntityByIdAsync(loan.User.Id);
                if (user != null) loan.User = user;
                else return BadRequest("User not found");
            }
            
            _repo.AddLoan(loan);

            var newLoan = _mapper.Map<LoanDto>(loan);

            // await _publishEndpoint.Publish(_mapper.Map<LoanCreated>(newLoan));

            var result = await _repo.SaveChangesAsync();

            if (!result) return BadRequest("Error while creating loan");

            return CreatedAtAction(nameof(GetLoanById), new { id = loan.Id }, _mapper.Map<LoanDto>(loan));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<LoanDto>> UpdateLoan(Guid id, UpdateLoanDto updateLoanDto)
        {
            var loan = await _repo.GetLoanEntityByIdAsync(id);

            if (loan == null) return NotFound();

            loan.Observation = updateLoanDto.Observation ?? loan.Observation;
            loan.ReturnDate = updateLoanDto.ReturnDate ?? loan.ReturnDate;

            // await _publishEndpoint.Publish(_mapper.Map<LoanUpdated>(loan));

            var result = await _repo.SaveChangesAsync();

            if (!result) return BadRequest("Error while updating loan");

            return _mapper.Map<LoanDto>(loan);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLoan(Guid id)
        {
            var loan = await _repo.GetLoanEntityByIdAsync(id);

            if (loan == null) return NotFound();

            _repo.RemoveLoan(loan);

            // await _publishEndpoint.Publish(_mapper.Map<LoanDeleted>(loan));

            var result = await _repo.SaveChangesAsync();

            if (!result) return BadRequest("Error while deleting loan");

            return NoContent();
        }
    }
}
