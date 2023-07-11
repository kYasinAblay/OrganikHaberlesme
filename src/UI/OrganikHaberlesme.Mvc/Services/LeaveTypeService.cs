﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using OrganikHaberlesme.Mvc.Contracts;
using OrganikHaberlesme.Mvc.Models.LeaveType;
using OrganikHaberlesme.Mvc.Services.Base;

namespace OrganikHaberlesme.Mvc.Services
{
    public class LeaveTypeService : BaseHttpService, ILeaveTypeService
    {
        private readonly IMapper _mapper;

        public LeaveTypeService(IMapper mapper, IClient client, ILocalStorageService localStorage)
            : base(localStorage, client)
        {
            _mapper = mapper;
        }

        public async Task<Response<int>> CreateLeaveType(CreateLeaveTypeVm createLeaveType)
        {
            try
            {
                var response = new Response<int>();
                var createLeaveTypeDto = _mapper.Map<CreateLeaveTypeDto>(createLeaveType);

                AddBearerToken();
                var apiResponse = await _client.LeaveTypesPOSTAsync(createLeaveTypeDto);

                if (apiResponse.Success)
                {
                    response.Data = apiResponse.Id;
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

        public async Task<Response<int>> DeleteLeaveType(int id)
        {
            try
            {
                AddBearerToken();
                await _client.LeaveTypesDELETEAsync(id);
                return new Response<int> { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }

        public async Task<LeaveTypeVm> GetLeaveTypeDetails(int id)
        {
            AddBearerToken();
            var leaveType = await _client.LeaveTypesGETAsync(id);
            return _mapper.Map<LeaveTypeVm>(leaveType);
        }

        public async Task<List<LeaveTypeVm>> GetLeaveTypes()
        {
            AddBearerToken();
            var leaveTypes = await _client.LeaveTypesAllAsync();
            return _mapper.Map<List<LeaveTypeVm>>(leaveTypes);
        }

        public async Task<Response<int>> UpdateLeaveType(int id, LeaveTypeVm leaveType)
        {
            try
            {
                var leaveTypeDto = _mapper.Map<LeaveTypeDto>(leaveType);
                AddBearerToken();
                await _client.LeaveTypesPUTAsync(leaveTypeDto); // TODO: resolve this id
                return new Response<int>() { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }
    }
}
