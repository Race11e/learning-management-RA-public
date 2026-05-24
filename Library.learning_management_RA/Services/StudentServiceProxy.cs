using Library.learning_management_RA.Models;

namespace Library.learning_management_RA.Services;

public class StudentServiceProxy
{
    
    private List<Student> students;
    
    public List<Student> Students => students;

    public StudentServiceProxy()
    {
        students = new List<Student>();
    }

    public int LastKey => Students.Any() ? Students.Select(s => s.Id).Max() : 0;
    
    public Student? Add(Student? student)
    {
        if (student == null)
        {
            return student;
        }

        if (student.Id == 0)
        {
            student.Id = LastKey + 1;
        }
        
        students.Add(student);
        return student;
    }
    
    // Update student
    public Student? Update(Student? student)
    {
        var existingStudent = students.FirstOrDefault(s => s.Id == student.Id);
        if (existingStudent != null)
        {
            existingStudent.Name = student.Name;
            existingStudent.Code = student.Code;
            existingStudent.Classification = student.Classification;
        }
        return existingStudent;
    }
    
    // Delete Student
    public void Delete(int id, CourseServiceProxy courseService)
    {
        var existingStudent = students.FirstOrDefault(s => s.Id == id);
        if (existingStudent != null)
        { 
            // remove from student list
            students.Remove(existingStudent);
            
            // remove student from all courses
            courseService.RemoveStudentFromSystem(id);
        }
    }
    
}