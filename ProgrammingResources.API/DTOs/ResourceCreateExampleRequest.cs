namespace ProgrammingResources.API.DTOs;

public class ResourceCreateExampleRequest
{
    public string? Text { get; set; }
    public string? Url { get; set; }
    public int? Page { get; set; }
    public int? TypeId { get; set; }
    public string? Language { get; set; }
}