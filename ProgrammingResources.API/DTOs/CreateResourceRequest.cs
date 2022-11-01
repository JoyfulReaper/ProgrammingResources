namespace ProgrammingResources.API.DTOs;

public class CreateResourceRequest
{
    public string Title { get; set; } = default!;
    public string? Url { get; set; }
    public string? Description { get; set; }
    public string? Langauge { get; set; }
    public string? Type { get; set; }
    public IList<string> Tags { get; set; } = new List<string>();
    public IList<ResourceCreateExampleDto> Examples {get; set;} = new List<ResourceCreateExampleDto>();
}