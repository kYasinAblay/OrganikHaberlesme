﻿using System.Collections.Generic;
using System.Threading.Tasks;

using OrganikHaberlesme.Application.Models.Identity;

namespace OrganikHaberlesme.Application.Contracts.Identity
{
    public interface IUserService
    {
        Task<List<Employee>> GetEmployees();

        Task<Employee> GetEmployee(string userId);
    }
}