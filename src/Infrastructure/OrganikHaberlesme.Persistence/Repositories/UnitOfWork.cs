using System;
using System.Threading.Tasks;

using OrganikHaberlesme.Application.Constants;
using OrganikHaberlesme.Application.Contracts.Persistence;

using Microsoft.AspNetCore.Http;

namespace OrganikHaberlesme.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProgramDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ILeaveAllocationRepository _leaveAllocationRepository;
        private ILeaveTypeRepository _leaveTypeRepository;
        private ILeaveRequestRepository _leaveRequestRepository;

        public UnitOfWork(ProgramDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public ILeaveAllocationRepository LeaveAllocationRepository => _leaveAllocationRepository ??= new LeaveAllocationRepository(_context);
        
        public ILeaveRequestRepository LeaveRequestRepository => _leaveRequestRepository ??= new LeaveRequestRepository(_context);
        
        public ILeaveTypeRepository LeaveTypeRepository => _leaveTypeRepository ??= new LeaveTypeRepository(_context);
        
        public async Task Save()
        {
            var username = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Uid)?.Value;
            await _context.SaveChangesAsync(username);
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
