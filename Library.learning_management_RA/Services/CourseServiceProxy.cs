using Library.learning_management_RA.Models;

namespace Library.learning_management_RA.Services;

public class CourseServiceProxy
{

    private List<Course> courses;
    
    public List<Course> Courses => courses;
    
    public CourseServiceProxy()
    {
        courses = new List<Course>();
    }
    

    
    private int Lastkey => courses.Any() ? Courses.Select(c => c.Id).Max() : 0;
    
    public Course Add(Course? course)
    {
        if (course == null)
        {
            return null;
        }

        if (course.Id == 0)
        {
            course.Id = Lastkey + 1;
        }
        // section automatic incrementing
        var existingSections = courses.Where(c => c.Code.Equals(course.Code, StringComparison.OrdinalIgnoreCase)).ToList();
        if (existingSections.Any())
        {
            course.Section = existingSections.Max(c => c.Section) + 1;
        }
        else
        {
            course.Section = 1;
        }
        
        courses.Add(course);
        return course;
    }

    // Used when deleting a student from uni system
    public void RemoveStudentFromSystem(int studentId)
    {
        foreach (var course in courses)
        {
            // remove student from every roster
            course.Roster?.RemoveAll(s => s.Id == studentId);

            if (course.Assignments != null)
            {
                foreach (var assignment in course.Assignments)
                {
                    assignment.Submissions?.RemoveAll(sub => sub.StudentId == studentId);
                }
            }
            
        }
    }
    
    // For copying course functionality
    public Course? CopyCourse(int ogId)
    {
        var og = courses.FirstOrDefault(c => c.Id == ogId);
        if (og == null) return null;

        var copy = new Course
        {
            Id = Lastkey + 1,
            Name = og.Name + " (Copy)",
            Code = og.Code,
            Semester = og.Semester,
            Section = courses.Where(c => c.Code == og.Code && c.Semester == og.Semester).Max(c => (int?)c.Section) ?? 0 + 1,
            Description = og.Description,
            Roster = new List<Student>(), // do not copy roster or announcement
            Announcements = new List<string>()
        };
        
        // Deep copy the assignment groups 
        copy.AssignmentGroups = og.AssignmentGroups.Select(ag => new AssignmentGroup
            { Id = ag.Id, Name = ag.Name, Weight = ag.Weight}).ToList();

        // deep copy assignments
        copy.Assignments = og.Assignments.Select(a => new Assignment
        {
            Id = a.Id,
            Name = a.Name,
            Description = a.Description,
            AvailablePoints = a.AvailablePoints,
            DueDate = a.DueDate,
            AssignmentGroupId = a.AssignmentGroupId,
            Submissions = new List<Submission>() // do not copy submissions
        }).ToList();
        
        // Deep copy modules
        copy.Modules = og.Modules.Select(m => new Module
        {
            Id = m.Id,
            Name = m.Name,
            Content = m.Content.Select(c =>
            {
                if (c is PageItem p)
                    return new PageItem 
                        { Id = p.Id, Name = p.Name, HtmlBody = p.HtmlBody } as ContentItem;
                if (c is FileItem f)
                    return new FileItem 
                        { Id = f.Id, Name = f.Name, FilePath = f.FilePath } as ContentItem;
                if (c is AssignmentItem a)
                    return new AssignmentItem
                        { Id = a.Id, Name = a.Name, AssignmentId = a.AssignmentId } as ContentItem;
                return null;
            }).Where(c => c != null).ToList()!
        }).ToList();
        
        courses.Add(copy);
        return copy;
    }

}