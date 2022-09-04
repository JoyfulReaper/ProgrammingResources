namespace ProgrammingResourcesApiClient.Models;

public class Example
{
    public int ExampleId { get; set; }
    public int ResourceId { get; set; }
    public string ExampleText { get; set; } = null!;
    public DateTime DateCreated { get; set; }
}
