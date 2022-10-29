﻿namespace ProgrammingResources.API.DTOs;

public class ResourceDto
{
    public int ResourceId { get; set; }
    public string Title { get; set; } = default!;
    public string? Url { get; set; }
    public string? Description { get; set; }
    public ProgrammingLanguageDto? ProgramingLanguage { get; set; }
    public TypeDto? Type { get; set; }
    public IList<ExampleDto> Examples { get; set; } = new List<ExampleDto>();
    public IList<TagDto> Tags { get; set; } = new List<TagDto>();
}
