namespace ProgrammingResourcesApiClient.Models;

public class Resource
{
    public int ResourceId { get; set; }
    public string Title { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string? Description { get; set; }
    public string? ProgrammingLanguage { get; set; } = null!;

    public DateTime DateCreated { get; set; }
}
