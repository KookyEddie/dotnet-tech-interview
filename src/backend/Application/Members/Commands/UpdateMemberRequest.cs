using System.Text.RegularExpressions;
using tech_interview_api.Application.Common;
using tech_interview_api.Domain;
using tech_interview_api.Infrastructure.Persistence;

namespace tech_interview_api.Application.Members.Commands;

public class UpdateMemberRequest : IRequest
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public string? EmailAddress { get; init; }
    public string? PhoneNumber { get; init; }
}

public partial class UpdateMemberRequestHandler : IRequestHandler<UpdateMemberRequest>
{
    private readonly ApplicationDbContext context;
    public UpdateMemberRequestHandler(ApplicationDbContext context) => this.context = context;

    public async Task<bool> Handle(UpdateMemberRequest request)
    {
        if (request.PhoneNumber is not null && !Regexes.PhoneNumberRegex().IsMatch(request.PhoneNumber)) { throw new ArgumentException("Phone number must be in this format (xxx) xxx-xxxx"); }

        if (request.EmailAddress is not null && !Regexes.EmailRegex().IsMatch(request.EmailAddress)) { throw new ArgumentException("Email address is not valid"); }

        // La fonction FindAsync cherche un élément selon la clé primaire, EmailAddress n'est pas la clé primaire, Id l'est.
        // Si le membre ID est null, throw une exception
        Member? memberToUpdate = await context.Members.FindAsync(request.Id) ?? throw new KeyNotFoundException($"No member found with ID {request.Id}");

        // S'assure que les champs ne soient pas null
        if (request.Name is not null) memberToUpdate.Name = request.Name;
        if (request.EmailAddress is not null) memberToUpdate.EmailAddress = request.EmailAddress;
        if (request.PhoneNumber is not null) memberToUpdate.PhoneNumber = request.PhoneNumber;

        context.Members.Update(memberToUpdate);
        await context.SaveChangesAsync();

        return true;
    }
}