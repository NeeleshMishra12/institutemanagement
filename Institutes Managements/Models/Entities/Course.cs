namespace Institutes_Managements.Models.Entities
{
    public class Course
    {
        public int? Id { get; set; }
        public string? CourseName { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? DurationWeeks { get; set; }
        public ICollection<Batch>? Batches { get; set; }
    }

}
