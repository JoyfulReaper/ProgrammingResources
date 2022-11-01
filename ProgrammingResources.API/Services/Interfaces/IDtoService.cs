﻿using ProgrammingResources.Library.Models;

namespace ProgrammingResources.API.Services;
public interface IDtoService
{
    Task<ProgrammingLanguage> GetOrAddLanguage(string langauge, string userId);
    Task<Tag> GetOrAddTag(string tag, string userId);
    Task<Library.Models.Type> GetOrAddType(string type, string userId);
}