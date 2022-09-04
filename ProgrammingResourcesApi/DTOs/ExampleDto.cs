namespace ProgrammingResourcesApi.DTOs;

public class ExampleDto
{
    public int ExampleId { get; set; }
    public int ResourceId { get; set; }
    public string ExampleText { get; set; } = null!;
    public DateTime DateCreated { get; set; }
}
