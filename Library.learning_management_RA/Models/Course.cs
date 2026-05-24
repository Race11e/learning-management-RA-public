namespace Library.learning_management_RA.Models;


public class Course
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public List<Student> Roster { get; set; }
    public List<Module>  Modules { get; set; }
    public List<Assignment> Assignments { get; set; }
    public string Semester { get; set; }
    public int Section { get; set; }
    public List<string> Announcements { get; set; } = new List<string>();
    // Added for assignment groups
    public List<AssignmentGroup> AssignmentGroups { get; set; } = new List<AssignmentGroup>();
    // Changing grade scale functionality
    public double GradeA { get; set; } = 90.0;
    public double GradeB { get; set; } = 80.0;
    public double GradeC { get; set; } = 70.0;
    public double GradeD { get; set; } = 60.0;
}