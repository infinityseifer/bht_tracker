namespace backend.Models
{
    public class BhtForm
    {
        public int Id { get; set; }

        public required string Role { get; set; }
        public required string StudentInitial { get; set; }
        public int StudentID { get; set; }
        public required string StudentStatus { get; set; }

        public required string TeacherName { get; set; }
        public required string TeacherEmail { get; set; }
        public required string ParentNotified { get; set; }
        public required string MainConcern { get; set; }
        public required string AdditionalConcern { get; set; }
        public required string Observation { get; set; }
        public List<string> StudentStrength { get; set; } = new();

        public List<string> TierOne { get; set; } = new();


        public List<string> TierTwo { get; set; } = new();
        public List<string> TierThree { get; set; } = new();
        public required string FBA { get; set; }
        public required string BehaviorData { get; set; }
        public required string BehaviorTime { get; set; }
        public required string BehaviorSubject { get; set; }
        
    }
}
