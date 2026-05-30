using LibraryMS.Application.Common.Interfaces;
using LibraryMS.Domain.Entities;
using LibraryMS.Infrastructure.Data;

namespace LibraryMS.Infrastructure.Repositories;

public class BookCopiesRepository(AppDbContext context) : GenericRepository<BookCopy>(context), IBookCopiesRepository;