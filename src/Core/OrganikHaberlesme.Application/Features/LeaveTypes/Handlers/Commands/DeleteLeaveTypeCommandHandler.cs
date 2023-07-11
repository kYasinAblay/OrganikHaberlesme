using System.Threading;
using System.Threading.Tasks;

using OrganikHaberlesme.Application.Contracts.Persistence;
using OrganikHaberlesme.Application.Exceptions;
using OrganikHaberlesme.Application.Features.LeaveTypes.Requests.Commands;
using OrganikHaberlesme.Domain;

using MediatR;

namespace OrganikHaberlesme.Application.Features.LeaveTypes.Handlers.Commands
{
    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var leaveType = await _leaveTypeRepository.Get(request.Id);

            if (leaveType == null)
            {
                throw new NotFoundException(nameof(LeaveType), request.Id);
            }

            await _leaveTypeRepository.Delete(leaveType);

            return Unit.Value;
        }
    }
}
