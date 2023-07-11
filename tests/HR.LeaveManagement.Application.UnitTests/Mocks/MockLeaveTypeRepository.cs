using System.Collections.Generic;

using OrganikHaberlesme.Application.Contracts.Persistence;
using OrganikHaberlesme.Domain;

using Moq;

namespace OrganikHaberlesme.Application.UnitTests.Mocks
{
    public static class MockLeaveTypeRepository
    {
        public static Mock<ILeaveTypeRepository> GetLeaveTypeRepository()
        {
            var leaveTypes = new List<LeaveType>
            {
                new LeaveType
                {
                    Id = 1,
                    Name = "Test Vacation",
                    DefaultDays = 10
                },
                new LeaveType
                {
                    Id = 2,
                    Name = "Test Party",
                    DefaultDays = 15
                }
            };

            var mockRepo = new Mock<ILeaveTypeRepository>();

            mockRepo.Setup(x => x.GetAll()).ReturnsAsync(leaveTypes);

            mockRepo.Setup(x => x.Add(It.IsAny<LeaveType>())).ReturnsAsync((LeaveType leaveType) =>
              {
                  leaveTypes.Add(leaveType);
                  return leaveType;
              });

            return mockRepo;
        }
    }
}

