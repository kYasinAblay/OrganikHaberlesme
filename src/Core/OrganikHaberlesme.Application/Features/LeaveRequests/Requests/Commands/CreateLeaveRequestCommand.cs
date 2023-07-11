using OrganikHaberlesme.Application.DTOs.LeaveRequest;
using OrganikHaberlesme.Application.Responses;

using MediatR;

namespace OrganikHaberlesme.Application.Features.LeaveRequests.Requests.Commands
{
    public class CreateLeaveRequestCommand : IRequest<BaseCommandResponse>
    {
        public CreateLeaveRequestDto LeaveRequestDto { get; set; }
    }
}
