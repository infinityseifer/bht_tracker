namespace backend.Models
{
    public class DrForm
    {
        public int Id { get; set; }

        public required string StudentLast { get; set; }
        public required string StudentFirst { get; set; }
        public int Grade { get; set; }
        public DateTime DateTime { get; set; }

        public required string HomeroomTeacher { get; set; }
        public required string TeacherIntervention { get; set; }
        public required string ProactiveConcern { get; set; }
        public required string Narrative { get; set; }
        public required string MinorProblemBehavior { get; set; }
        public required string MajorProblemBehavior { get; set; }
        public required string NextSteps { get; set; }
        public required string SELCompetency { get; set; }
    }
}
