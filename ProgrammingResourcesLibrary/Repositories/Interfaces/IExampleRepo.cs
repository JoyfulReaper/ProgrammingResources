﻿using ProgrammingResourcesLibrary.Models;

namespace ProgrammingResourcesLibrary.Repositories.Interfaces;
public interface IExampleRepo
{
    Task Delete(int exampleId);
    Task<IEnumerable<Example>> Get(int resourceId);
    Task Save(Example example);
}