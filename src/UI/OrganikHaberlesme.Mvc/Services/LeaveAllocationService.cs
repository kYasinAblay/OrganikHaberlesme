using System;
using System.Threading.Tasks;

using OrganikHaberlesme.Mvc.Contracts;
using OrganikHaberlesme.Mvc.Services.Base;

namespace OrganikHaberlesme.Mvc.Services
{
    public class LeaveAllocationService : BaseHttpService, ILeaveAllocationService
    {
        public LeaveAllocationService(ILocalStorageService localStorage, IClient client)
            : base(localStorage, client)
        {
        }

        public async Task<Response<int>> CreateLeaveAllocations(int leaveTypeId)
        {
            try
            {
                var response = new Response<int>();
                var createLeaveAllocation = new CreateLeaveAllocationDto { LeaveTypeId = leaveTypeId };

                AddBearerToken();

                var apiResponse = await _client.LeaveAllocationsPOSTAsync(createLeaveAllocation);
                if (apiResponse.Success)
                {
                    response.Success = true;
                }
                else
                {
                    foreach (var error in apiResponse.Errors)
                    {
                        response.ValidationErrors += error + Environment.NewLine;
                    }
                }

                return response;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }
    }
}
