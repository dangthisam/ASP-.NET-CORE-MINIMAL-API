using bookapi_minimal.Contracts;
using bookapi_minimal.Interfaces;

namespace bookapi_minimal.Endpoints
{
    public static class BookEndPoint
    {
        public static IEndpointRouteBuilder MapBookEndPoint(this IEndpointRouteBuilder app)
        {
            // endpoint tạo book mới
            app.MapPost("/books", async (
                CreateBookRequest createBookRequest,
                IBookService bookService
            ) =>
            {
                var result = await bookService.AddBookAsync(createBookRequest);
                return Results.Created($"/books/{result.Id}", result);
            });
       
       app.MapGet("/books/{id:guid}", async (
                Guid id,
                IBookService bookService
            ) =>
            {
                var result = await bookService.GetBookByIdAsync(id);
                return Results.Ok(result);
            });

            app.MapGet("/books", async (
                IBookService bookService
            ) =>
            {
                var result = await bookService.GetBooksAsync();
                return Results.Ok(result);
            });

            app.MapPut("/books/{id:guid}", async (
                Guid id,
                UpdateBookRequest updateBookRequest,
                IBookService bookService
            ) =>
            {
                var result = await bookService.UpdateBookAsync(id, updateBookRequest);
                return Results.Ok(result);
            });
            app.MapDelete("/books/{id:guid}", async (
                Guid id,
                IBookService bookService
            ) =>
            {
                var isDeleted = await bookService.DeleteBookAsync(id);
                return isDeleted ? Results.NoContent() : Results.NotFound();
            });
            
            return app;
        }
    }
}
