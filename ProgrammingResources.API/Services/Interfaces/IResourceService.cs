using ProgrammingResources.API.DTOs;
using ProgrammingResources.Library.Models;

namespace ProgrammingResources.API.Services.Interfaces;
public interface IResourceService
{
    Task<ResourceDto> GetResourceDto(Resource resource);
}