namespace Institutes_Managements.Models.Entities
{
    public class Batch
    {
        public int Id { get; set; }
        public string? BatchName { get; set; }
        public int? CourseId { get; set; }
        public Course? Course { get; set; }
        public string? BatchTiming { get; set; }
        public int? Capacity { get; set; }

        public ICollection<User>? Instructors { get; set; }
        public ICollection<User>? Students { get; set; }
    }

}
