namespace ProgrammingResourcesLibrary.Models;

public class Resource
{
    public int ResourceId { get; set; }
    public string Title { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string? Description { get; set; }
    public string? ProgrammingLanguage { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateDeleted { get; set; }
}
