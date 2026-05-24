namespace Library.learning_management_RA.Models;

public class Student : LibraryLM.User
{
    public Classification Classification { get; set; }
}

public enum Classification
{
    Unknown, Freshman, Sophomore, Junior, Senior
}