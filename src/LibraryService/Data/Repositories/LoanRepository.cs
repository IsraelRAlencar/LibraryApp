using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryService.Data.Interfaces;
using LibraryService.DTOs.LoanDTOs;
using LibraryService.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Data.Repositories;

public class LoanRepository : ILoanRepository
{
    private readonly LibraryDbContext _context;
    private readonly IMapper _mapper;

    public LoanRepository(LibraryDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void AddLoan(Loan Loan)
    {
        _context.Loans.Add(Loan);
    }

    public async Task<LoanDto> GetLoanByIdAsync(Guid id)
    {
        return await _context.Loans.ProjectTo<LoanDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<Loan> GetLoanEntityByIdAsync(Guid id)
    {
        return await _context.Loans.FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<List<LoanDto>> GetLoansAsync()
    {
        return await _context.Loans.ProjectTo<LoanDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public void RemoveLoan(Loan Loan)
    {
        _context.Loans.Remove(Loan);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
