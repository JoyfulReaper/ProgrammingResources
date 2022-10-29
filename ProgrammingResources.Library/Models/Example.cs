using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingResources.Library.Models;
public class Example
{
    public int ExampleId { get; set; }
    public int ResourceId { get; set; }
    public int MyProperty { get; set; }
    public string? Text { get; set; }
    public string? Url { get; set; }
    public int? Page { get; set; }
    public int? TypeId { get; set; }
    public int? ProgrammingLanguageId { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateDeleted { get; set; }
    public string UserId { get; set; } = default!;
}
