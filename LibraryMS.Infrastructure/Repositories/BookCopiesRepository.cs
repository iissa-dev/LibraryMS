namespace LibraryMS.Infrastructure.Repositories;

public class BookCopiesRepository(AppDbContext context) : GenericRepository<BookCopy>(context), IBookCopiesRepository;