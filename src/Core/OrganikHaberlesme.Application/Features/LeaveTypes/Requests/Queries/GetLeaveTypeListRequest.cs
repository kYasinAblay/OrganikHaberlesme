using System.Collections.Generic;

using OrganikHaberlesme.Application.DTOs.LeaveType;

using MediatR;

namespace OrganikHaberlesme.Application.Features.LeaveTypes.Requests.Queries
{
    public class GetLeaveTypeListRequest : IRequest<List<LeaveTypeDto>>
    {
    }
}
