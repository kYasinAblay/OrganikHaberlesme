using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using OrganikHaberlesme.Application.Constants;
using OrganikHaberlesme.Application.Contracts.Identity;
using OrganikHaberlesme.Application.Contracts.Persistence;
using OrganikHaberlesme.Application.DTOs.LeaveRequest;
using OrganikHaberlesme.Application.Features.LeaveRequests.Requests.Queries;
using OrganikHaberlesme.Domain;

using MediatR;

using Microsoft.AspNetCore.Http;

namespace OrganikHaberlesme.Application.Features.LeaveRequests.Handlers.Queries
{
    public class GetLeaveRequestListRequestHandler : IRequestHandler<GetLeaveRequestListRequest, List<LeaveRequestListDto>>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;

        public GetLeaveRequestListRequestHandler(
            ILeaveRequestRepository leaveRequestRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IUserService userService)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListRequest request, CancellationToken cancellationToken)
        {
            var leaveRequests = new List<LeaveRequest>();
            var requests = new List<LeaveRequestListDto>();

            if (request.IsLoggedInUser)
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(x => x.Type == CustomClaimTypes.Uid)?.Value;

                if (userId is not null)
                {
                    leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails(userId);

                    var employee = await _userService.GetEmployee(userId);

                    requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);

                    foreach (var req in requests)
                    {

                    }
                }
                else
                {
                    leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails();
                    requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);

                    foreach (var req in requests)
                    {
                        req.Employee = await _userService.GetEmployee(req.RequestingEmployeeId);
                    }
                }

            }

            return requests;
        }
    }
}
