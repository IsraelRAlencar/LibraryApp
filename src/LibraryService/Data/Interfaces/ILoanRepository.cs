using LibraryService.DTOs.LoanDTOs;
using LibraryService.Entities;

namespace LibraryService.Data.Interfaces;

public interface ILoanRepository
{
    Task<List<LoanDto>> GetLoansAsync();
    Task<LoanDto> GetLoanByIdAsync(Guid id);
    Task<Loan> GetLoanEntityByIdAsync(Guid id);
    void AddLoan(Loan Loan);
    void RemoveLoan(Loan Loan);
    Task<bool> SaveChangesAsync();
}
