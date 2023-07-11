﻿using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using OrganikHaberlesme.Application.Contracts.Persistence;
using OrganikHaberlesme.Application.DTOs.LeaveType.Validators;
using OrganikHaberlesme.Application.Exceptions;
using OrganikHaberlesme.Application.Features.LeaveTypes.Requests.Commands;
using OrganikHaberlesme.Domain;

using MediatR;

namespace OrganikHaberlesme.Application.Features.LeaveTypes.Handlers.Commands
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.LeaveTypeDto, cancellationToken);

            if (validationResult.IsValid == false)
            {
                throw new ValidationException(validationResult);
            }

            var leaveType = await _leaveTypeRepository.Get(request.LeaveTypeDto.Id);

            if (leaveType == null)
            {
                throw new NotFoundException(nameof(LeaveType), request.LeaveTypeDto.Id);
            }

            _mapper.Map(request.LeaveTypeDto, leaveType);

            await _leaveTypeRepository.Update(leaveType);

            return Unit.Value;
        }
    }
}
