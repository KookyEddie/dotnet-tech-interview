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
        var member = new Member
        {
            Name = request.Name,
            EmailAddress = request.EmailAddress,
            PhoneNumber = request.PhoneNumber
        };

        context.Members.Add(member);
        return await context.SaveChangesAsync() > 0;
    }
}