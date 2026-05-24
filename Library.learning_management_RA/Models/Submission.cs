namespace Library.learning_management_RA.Models;

public class Submission
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int AssignmentId { get; set; }
    public string Content { get; set; }
    public DateTime SubmissionDate { get; set; }
    public double? Grade { get; set; }
    public string Comment { get; set; }
    public string FilePath { get; set; } 
}