using OrganikHaberlesme.Application.Contracts.Persistence;

using Moq;

namespace OrganikHaberlesme.Application.UnitTests.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetUnitOfWork()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var mockLeaveRepo = MockLeaveTypeRepository.GetLeaveTypeRepository();

            unitOfWork.Setup(x => x.LeaveTypeRepository).Returns(mockLeaveRepo.Object);

            return unitOfWork;
        }
    }
}
