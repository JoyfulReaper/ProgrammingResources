namespace ProgrammingResourcesApiClient.Models;

public class ResourceTagRequest
{
    public int ResourceId { get; set; }
    public string TagName { get; set; } = null!;
}
