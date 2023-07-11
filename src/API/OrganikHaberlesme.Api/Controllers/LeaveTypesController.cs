﻿using System.Collections.Generic;
using System.Threading.Tasks;

using OrganikHaberlesme.Application.DTOs.LeaveType;
using OrganikHaberlesme.Application.Features.LeaveTypes.Requests.Commands;
using OrganikHaberlesme.Application.Features.LeaveTypes.Requests.Queries;
using OrganikHaberlesme.Application.Responses;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OrganikHaberlesme.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LeaveTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<LeaveTypeDto>>> Get()
        {
            var leaveTypes = await _mediator.Send(new GetLeaveTypeListRequest());
            return Ok(leaveTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveTypeDto>> Get(int id)
        {
            var leaveType = await _mediator.Send(new GetLeaveTypeDetailRequest { Id = id });
            return Ok(leaveType);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateLeaveTypeDto leaveType)
        {
            var command = new CreateLeaveTypeCommand { CreateLeaveTypeDto = leaveType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Put([FromBody] LeaveTypeDto leaveType)
        {
            var command = new UpdateLeaveTypeCommand { LeaveTypeDto = leaveType };
            _ = await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteLeaveTypeCommand { Id = id };
            _ = await _mediator.Send(command);
            return NoContent();
        }
    }
}
