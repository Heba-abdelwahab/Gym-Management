using Domain.Enums;

namespace Shared
{
    public record CoachPendingDto {
        public int CoachId { get; set; }
        public string Name { get; set; }
        public string Specializations { get; set; }
        public int Capcity { get; set; }
        public string About { get; set; }
        public IEnumerable<WorkDayPendingCoachDto> WorkDays { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string ApplicationCVUrl { get; set; }
        public string ImageUrl {  get; set; }
    }

}
