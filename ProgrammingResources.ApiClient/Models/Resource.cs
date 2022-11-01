using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingResources.ApiClient.Models;
public class Resource
{
    public string Title { get; set; } = default!;
    public string? Url { get; set; }
    public string? Description { get; set; }
    public string? Langauge { get; set; }
    public string? Type { get; set; }
    public IList<Example> Examples { get; set; } = new List<Example>();
    public IList<string> Tags { get; set; } = new List<string>();
}
