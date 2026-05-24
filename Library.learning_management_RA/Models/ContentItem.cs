namespace Library.learning_management_RA.Models;
// allows modules to hold other types besides strings
public abstract class ContentItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class PageItem : ContentItem
{
    public string HtmlBody { get; set; } = string.Empty;
}

public class FileItem : ContentItem
{
    public string FilePath { get; set; } = string.Empty;
}

public class AssignmentItem : ContentItem
{
    public int AssignmentId { get; set; }
}