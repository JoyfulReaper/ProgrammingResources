using ProgrammingResources.Library.Models;
using ProgrammingResources.Library.Services.Repos;
using Type = ProgrammingResources.Library.Models.Type;

namespace ProgrammingResources.API.Services;

public class DtoService : IDtoService
{
    private readonly IProgrammingLanguageRepo _languageRepo;
    private readonly ITypeRepo _typeRepo;
    private readonly ITagRepo _tagRepo;
    private readonly IExampleRepo _exampleRepo;

    public DtoService(IProgrammingLanguageRepo languageRepo,
        ITypeRepo typeRepo,
        ITagRepo tagRepo,
        IExampleRepo exampleRepo)
    {
        _languageRepo = languageRepo;
        _typeRepo = typeRepo;
        _tagRepo = tagRepo;
        _exampleRepo = exampleRepo;
    }

    public async Task<ProgrammingLanguage> GetOrAddLanguage(string langauge, string userId)
    {
        var plDb = await _languageRepo.Get(langauge);
        if (plDb is null)
        {
            plDb = await _languageRepo.Add(new ProgrammingLanguage
            {
                Language = langauge,
                UserId = userId,
            });
        }

        return plDb;
    }

    public async Task<Type> GetOrAddType(string type, string userId)
    {
        var typeDb = await _typeRepo.Get(type);
        if(typeDb is null)
        {
            typeDb = await _typeRepo.Add(new Type
            {
                Name = type,
                UserId = userId,
            });
        }

        return typeDb;
    }

    public async Task<Tag> GetOrAddTag(string tag, string userId)
    {
        var tagDb = await _tagRepo.Get(tag);
        if(tagDb is null)
        {
            tagDb = await _tagRepo.Add(new Tag
            {
                Name = tag,
                UserId = userId,
            });
        }

        return tagDb;
    }
}