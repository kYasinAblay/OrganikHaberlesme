﻿using System.Collections.Generic;
using System.Threading.Tasks;

using OrganikHaberlesme.Domain;

namespace OrganikHaberlesme.Application.Contracts.Persistence
{
    public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
    {
        Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id);
        
        Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails();

        Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId);

        Task<bool> AllocationExists(string userId, int leaveTypeId, int period);

        Task AddAllocations(IEnumerable<LeaveAllocation> allocations);

        Task<LeaveAllocation?> GetUserAllocations(string userId, int leaveTypeId);
    }
}
