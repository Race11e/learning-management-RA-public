namespace Library.learning_management_RA.Models;

public class Module
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    // changed to allow for different content items
    public List<ContentItem> Content { get; set; } = new List<ContentItem>();
}