﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OrganikHaberlesme.Application.Contracts.Identity;
using OrganikHaberlesme.Application.Models.Identity;
using OrganikHaberlesme.Identity.Models;

using Microsoft.AspNetCore.Identity;

namespace OrganikHaberlesme.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            var employees = await _userManager.GetUsersInRoleAsync("Employee");

            return employees
                .Select(x => new Employee
                {
                    Id = x.Id,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                })
                .ToList();
        }

        public async Task<Employee> GetEmployee(string userId)
        {
            var employee = await _userManager.FindByIdAsync(userId);

            return new Employee
            {
                Email = employee.Email, Id = employee.Id, FirstName = employee.FirstName, LastName = employee.LastName,
            };
        }
    }
}
