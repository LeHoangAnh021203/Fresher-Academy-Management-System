namespace FamsAPI.ViewModel
{
    public class AssessmentViewModel
    {
        public string AssessmentID { get; set; }
        public int QuizCount { get; set; }
        public double QuizPercent { get; set; }
        public int AssignmentCount { get; set; }
        public double AssignmentPercent { get; set; }
        public double FinalTheoryPercent { get; set; }
        public double FinalPracticePercent { get; set; }
        public string TopicCode { get; set; }
    }
}
