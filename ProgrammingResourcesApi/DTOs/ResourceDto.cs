namespace ProgrammingResourcesApi.DTOs;

public class ResourceDto
{
    public int ResourceId { get; set; }
    public string Title { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime DateCreated { get; set; }
}
