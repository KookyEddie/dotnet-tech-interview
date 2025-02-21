using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using tech_interview_api.Application.Books.Models;
using tech_interview_api.Application.Common;
using tech_interview_api.Infrastructure.Persistence;

namespace tech_interview_api.Application.Books.Commands;

public class GetBooksCommand : IRequest<List<BookDto>> { }

public class GetBooksCommandHandler : IRequestHandler<GetBooksCommand, List<BookDto>>
{
    private readonly ApplicationDbContext applicationDbContext;
    public GetBooksCommandHandler(ApplicationDbContext applicationDbContext) => this.applicationDbContext = applicationDbContext;

    // Simple ajout de la fonction OrderBy pour trier les livres par leur titre de facon alphabetique
    // avant de recuperer la liste
    public Task<List<BookDto>> Handle(GetBooksCommand request) => 
        applicationDbContext.Books.OrderBy(book => book.Title)
        .Select(book => new BookDto
    {
        Id = book.Id,
        Title = book.Title,
        Author = book.Author,
        // Ajout du champ isAvailable pour le ui (montrer si le livre est emprunté ou non)
        isAvailable = !applicationDbContext.Loans.Any(loan => loan.BookId == book.Id)
    }).ToListAsync();
}