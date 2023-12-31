﻿using System;
using System.Threading.Tasks;

namespace OrganikHaberlesme.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        ILeaveAllocationRepository LeaveAllocationRepository { get; }

        ILeaveRequestRepository LeaveRequestRepository { get; }

        ILeaveTypeRepository LeaveTypeRepository { get; }

        Task Save();
    }
}
