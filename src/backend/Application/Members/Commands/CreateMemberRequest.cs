using Microsoft.EntityFrameworkCore;
using tech_interview_api.Application.Common;
using tech_interview_api.Domain;
using tech_interview_api.Infrastructure.Persistence;

namespace tech_interview_api.Application.Members.Commands;

public class CreateMemberRequest : IRequest
{
    public required string Name { get; init; }
    public string? EmailAddress { get; init; }
    public string? PhoneNumber { get; init; }
}

public class CreateMemberRequestHandler : IRequestHandler<CreateMemberRequest>
{
    private readonly ApplicationDbContext context;
    public CreateMemberRequestHandler(ApplicationDbContext context) => this.context = context;

    public async Task<bool> Handle(CreateMemberRequest request)
    {
        // Vérifie avant si le nouveau email du membre correspond à un email
        // déjà inscrit dans la base de données
        // En cas d'erreur, retourn un exception 
        if (await context.Members.AnyAsync(member => member.EmailAddress == request.EmailAddress)) {
            throw new InvalidOperationException("Un membre avec ce email existe déjà.");
        }

        var member = new Member
        {
            Name = request.Name,
            EmailAddress = request.EmailAddress,
            PhoneNumber = request.PhoneNumber
        };

        await context.Members.AddAsync(member);
        return await context.SaveChangesAsync() > 0;
    }
}