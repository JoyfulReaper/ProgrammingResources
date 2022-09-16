using System.ComponentModel.DataAnnotations;

namespace ProgrammingResourcesApi.DTOs;

public class TagResourceDto
{
    public int ResourceId { get; set; }

    [Required]
    public string TagName { get; set; } = null!;
}
