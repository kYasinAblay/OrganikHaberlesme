using FluentValidation;

using OrganikHaberlesme.Application.Contracts.Persistence;

namespace OrganikHaberlesme.Application.DTOs.LeaveRequest.Validators
{
    public class UpdateLeaveRequestDtoValidator : AbstractValidator<UpdateLeaveRequestDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public UpdateLeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;

            Include(new ILeaveRequestDtoValidator(leaveTypeRepository));

            RuleFor(p => p.Id)
                .NotNull()
                .WithMessage("{PropertyName} must be present.");
        }
    }
}
