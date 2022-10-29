﻿namespace ProgrammingResources.API.DTOs;

public class CreateExampleRequest
{
    public int ResourceId { get; set; }
    public string? Text { get; set; }
    public string? Url { get; set; }
    public int? Page { get; set; }
    public int? TypeId { get; set; }
    public int? ProgrammingLanguageId { get; set; }
}
