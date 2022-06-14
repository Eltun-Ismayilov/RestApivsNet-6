using Domain.Base;

namespace Application.VM
{
    public class ActivityVm: BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string City { get; set; }
        public string Venue { get; set; }
        public string HostUsername { get; set; }
        public bool IsCancelled { get; set; }
        public ICollection<ProfileVm> Profiles { get; set; }

    }
}
