namespace tech_interview_api.Application.Books.Models;

public class BookDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Author { get; set; }
    // Ajout du champ isAvailable pour le ui (disponibilite du livre)
    public bool isAvailable { get; set; }
}