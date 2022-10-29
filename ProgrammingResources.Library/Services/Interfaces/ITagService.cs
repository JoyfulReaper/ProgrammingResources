﻿using ProgrammingResources.Library.Models;

namespace ProgrammingResources.Library.Services.Interfaces;
public interface ITagService
{
    Task<Tag> Add(Tag tag);
    Task Delete(int tagId);
    Task<Tag?> Get(int tagId);
    Task<IEnumerable<Tag>> GetAll();
}