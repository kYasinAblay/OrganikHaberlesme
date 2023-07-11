using OrganikHaberlesme.Domain.Common;

namespace OrganikHaberlesme.Domain
{
    public class LeaveType : BaseDomainEntity
    {
        public string Name { get; set; }

        public int DefaultDays { get; set; }
    }
}
