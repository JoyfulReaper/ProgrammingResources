using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingResources.Library.Models;
public class Resource
{
    public int ResourceId { get; set; }
    public string Title { get; set; } = default!;
    public string? Url { get; set; }
    public string? Description { get; set; }
    public int? ProgrammingLanguageId { get; set; }
    public int? TypeId { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateDeleted { get; set; }
    public string UserId { get; set; } = default!;
}
