using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using tech_interview_api.Application.Common;
using tech_interview_api.Application.Members.Models;
using tech_interview_api.Infrastructure.Persistence;

namespace tech_interview_api.Application.Members.Commands;

public class GetMembersRequest : IRequest<List<MemberDto>> { }

public class GetMembersRequestHandler : IRequestHandler<GetMembersRequest, List<MemberDto>>
{
    private readonly ApplicationDbContext context;
    public GetMembersRequestHandler(ApplicationDbContext context) => this.context = context;

    public async Task<List<MemberDto>> Handle(GetMembersRequest request)
    {
        return (await context.Members
                .AsNoTracking()
                .Select(member => new MemberDto
                {
                    Id = member.Id,
                    Name = member.Name,
                    EmailAddress = member.EmailAddress,
                    PhoneNumber = member.PhoneNumber
                })
                .ToListAsync());
                // J'ai enlevé le regex qui vérifie que le téléphone est correcte, mais si vous voulez
                // effectuer la vérification quand même, une option serait de sélectionner tous les membres comme ci-haut,
                //  et retourner ceci : return members.Where(member => Regexes.PhoneNumberRegex().IsMatch(member.PhoneNumber ?? "")).ToList();
    }
}