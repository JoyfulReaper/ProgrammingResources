using ProgrammingResourcesLibrary.Models;

namespace ProgrammingResourcesApi.DTOs;

public class ResourceDto
{
    public int ResourceId { get; set; }
    public string Title { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string? Description { get; set; }
    public string? ProgrammingLanguage { get; set; }
    public List<Tag>? Tags { get; set; }
    public DateTime DateCreated { get; set; }
}
