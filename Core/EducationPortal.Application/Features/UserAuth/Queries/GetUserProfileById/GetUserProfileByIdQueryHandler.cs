using EducationPortal.Application.Abstractions.IServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Features.UserAuth.Queries.GetUserProfileById
{
    public class GetUserProfileByIdQueryHandler : IRequestHandler<GetUserProfileByIdQueryRequest, GetUserProfileByIdQueryResponse>
    {
        readonly IProfileService _profileService;

        public GetUserProfileByIdQueryHandler(IProfileService profileService)
        {
            _profileService = profileService;
        }

        public async Task<GetUserProfileByIdQueryResponse> Handle(GetUserProfileByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var userProfile = await _profileService.GetUserProfileAsync();


            return new GetUserProfileByIdQueryResponse
            {
                UserProfile = userProfile
            };

        }
    }
}
