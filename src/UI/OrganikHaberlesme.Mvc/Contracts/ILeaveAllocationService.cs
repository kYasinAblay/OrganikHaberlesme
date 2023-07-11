using System.Threading.Tasks;

using OrganikHaberlesme.Mvc.Services.Base;

namespace OrganikHaberlesme.Mvc.Contracts
{
    public interface ILeaveAllocationService
    {
        Task<Response<int>> CreateLeaveAllocations(int leaveTypeId);
    }
}
