using bookapi_minimal.Interfaces;
using bookapi_minimal.Contracts;
using System;
using bookapi_minimal.AppContext;
namespace bookapi_minimal.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<BookService> _logger;

        public BookService(ApplicationContext context, ILogger<BookService> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Constructor to initialize the database context and logger and crate new book
        public async Task<BookResponse> AddBookAsync(CreateBookRequest createBookRequest)
        {
            try
            {
                var book = new Models.BookModel
                {
                    Id = Guid.NewGuid(),
                    Title = createBookRequest.Title,
                    Author = createBookRequest.Author,
                    Description = createBookRequest.Description,
                    Category = createBookRequest.Category,
                    Language = createBookRequest.Language,
                    TotalPages = createBookRequest.TotalPages
                };

                // add the book to the database
                _context.Books.Add(book);
            await     _context.SaveChangesAsync();
               _logger.LogInformation($"Book with ID {book.Id} added successfully.");

                return new BookResponse
                {
                  Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Description = book.Description,
                    Category = book.Category,
                    Language = book.Language,
                    TotalPages = book.TotalPages
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddBookAsync)}: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteBookAsync(Guid id)
        {
            try
            {
                var book = _context.Books.Find(id);
                if (book == null)
                {
                    _logger.LogWarning($"Book with ID {id} not found.");
                    return false;
                }

                _context.Books.Remove(book);
            await    _context.SaveChangesAsync();
               _logger.LogInformation($"Book with ID {id} deleted successfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(DeleteBookAsync)}: {ex.Message}");
                throw;
            }
        }

        public async Task<BookResponse> GetBookByIdAsync(Guid id)
        {
            try
            {
                var book=_context.Books.Find(id);
                if(book==null)
                {
                    _logger.LogWarning($"Book with ID {id} not found.");
                    return null;
                }
                return new BookResponse
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Description = book.Description,
                    Category = book.Category,
                    Language = book.Language,
                    TotalPages = book.TotalPages
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetBookByIdAsync)}: {ex.Message}");
                throw;
            }
        }

        public Task<IEnumerable<BookResponse>> GetBooksAsync()
        {
           try
            {
                var books = _context.Books.ToList();
                var bookResponses = books.Select(book => new BookResponse
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Description = book.Description,
                    Category = book.Category,
                    Language = book.Language,
                    TotalPages = book.TotalPages
                });

                return Task.FromResult(bookResponses);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetBooksAsync)}: {ex.Message}");
                throw;
            }
        }

        public async Task<BookResponse> UpdateBookAsync(Guid id, UpdateBookRequest updateBookRequest)
        {
            try
            {
                var bookExisting = _context.Books.Find(id);
                if (bookExisting == null)
                {
                    _logger.LogWarning($"Book with ID {id} not found.");
                    return null;
                }
                // Update the book details
                bookExisting.Title = updateBookRequest.Title;
                bookExisting.Author = updateBookRequest.Author;
                bookExisting.Description = updateBookRequest.Description;
                bookExisting.Category = updateBookRequest.Category;
                bookExisting.Language = updateBookRequest.Language;
                bookExisting.TotalPages = updateBookRequest.TotalPages;
               
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Book with ID {id} updated successfully.");
                return new BookResponse
                {
                    Id = bookExisting.Id,
                    Title = bookExisting.Title,
                    Author = bookExisting.Author,
                    Description = bookExisting.Description,
                    Category = bookExisting.Category,
                    Language = bookExisting.Language,
                    TotalPages = bookExisting.TotalPages
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateBookAsync)}: {ex.Message}");
                throw;
            }
        }
    }
}