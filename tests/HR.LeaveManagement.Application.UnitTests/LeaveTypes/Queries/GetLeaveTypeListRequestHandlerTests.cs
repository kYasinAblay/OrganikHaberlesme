using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using OrganikHaberlesme.Application.Contracts.Persistence;
using OrganikHaberlesme.Application.DTOs.LeaveType;
using OrganikHaberlesme.Application.Features.LeaveTypes.Handlers.Queries;
using OrganikHaberlesme.Application.Features.LeaveTypes.Requests.Queries;
using OrganikHaberlesme.Application.Profiles;
using OrganikHaberlesme.Application.UnitTests.Mocks;

using Moq;

using Shouldly;

using Xunit;

namespace OrganikHaberlesme.Application.UnitTests.LeaveTypes.Queries
{
    public class GetLeaveTypeListRequestHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ILeaveTypeRepository> _mockRepo;

        public GetLeaveTypeListRequestHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfiles>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockRepo = MockLeaveTypeRepository.GetLeaveTypeRepository();
        }

        [Fact]
        public async Task GetLeaveTypeListTest()
        {
            var handler = new GetLeaveTypeListRequestHandler(_mockRepo.Object, _mapper);

            var result = await handler.Handle(new GetLeaveTypeListRequest(), CancellationToken.None);

            result.ShouldBeOfType<List<LeaveTypeDto>>();
            result.Count.ShouldBe(2);
        }
    }
}

