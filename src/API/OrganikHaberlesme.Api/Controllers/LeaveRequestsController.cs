using System.Collections.Generic;
using System.Threading.Tasks;

using OrganikHaberlesme.Application.DTOs.LeaveRequest;
using OrganikHaberlesme.Application.Features.LeaveRequests.Requests.Commands;
using OrganikHaberlesme.Application.Features.LeaveRequests.Requests.Queries;
using OrganikHaberlesme.Application.Responses;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OrganikHaberlesme.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveRequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<LeaveRequestListDto>>> Get(bool isLoggedInUser = false)
        {
            var leaveRequests = await _mediator.Send(new GetLeaveRequestListRequest() { IsLoggedInUser = isLoggedInUser });
            return Ok(leaveRequests);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveRequestDto>> Get(int id)
        {
            var leaveRequest = await _mediator.Send(new GetLeaveRequestDetailRequest { Id = id });
            return Ok(leaveRequest);
        }

        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateLeaveRequestDto leaveRequest)
        {
            var command = new CreateLeaveRequestCommand { LeaveRequestDto = leaveRequest };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] UpdateLeaveRequestDto leaveRequest)
        {
            var command = new UpdateLeaveRequestCommand { Id = id, UpdateLeaveRequestDto = leaveRequest };
            _ = await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("changeapproval/{id}")]
        public async Task<ActionResult> ChangeApproval([FromRoute] int id, [FromBody] ChangeLeaveRequestApprovalDto leaveRequest)
        {
            var command = new UpdateLeaveRequestCommand { Id = id, ChangeLeaveRequestApprovalDto = leaveRequest };
            _ = await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteLeaveRequestCommand { Id = id };
            _ = await _mediator.Send(command);
            return NoContent();
        }
    }
}
