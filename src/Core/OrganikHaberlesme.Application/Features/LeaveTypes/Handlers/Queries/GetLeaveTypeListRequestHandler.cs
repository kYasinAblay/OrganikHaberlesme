using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using OrganikHaberlesme.Application.Contracts.Persistence;
using OrganikHaberlesme.Application.DTOs.LeaveType;
using OrganikHaberlesme.Application.Features.LeaveTypes.Requests.Queries;

using MediatR;

namespace OrganikHaberlesme.Application.Features.LeaveTypes.Handlers.Queries
{
    public class GetLeaveTypeListRequestHandler : IRequestHandler<GetLeaveTypeListRequest, List<LeaveTypeDto>>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public GetLeaveTypeListRequestHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypeListRequest request, CancellationToken cancellationToken)
        {
            var leaveTypes = await _leaveTypeRepository.GetAll();
            return _mapper.Map<List<LeaveTypeDto>>(leaveTypes);
        }
    }
}
