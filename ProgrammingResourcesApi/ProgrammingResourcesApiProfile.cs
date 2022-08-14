using AutoMapper;
using ProgrammingResourcesApi.DTOs;
using ProgrammingResourcesLibrary.Models;

namespace ProgrammingResourcesApi;

public class ProgrammingResourcesApiProfile : Profile
{
	public ProgrammingResourcesApiProfile()
	{
		CreateMap<Resource, ResourceDto>();
		CreateMap<ResourceDto, Resource>();
	}
}
