using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingResources.Library.Models;

public class ProgrammingLanguage
{
    public int ProgrammingLanguageId { get; set; }
    public string Language { get; set; } = default!;
    public DateTime DateAdded { get; set; }
    public DateTime? DateDeleted { get; set; }
    public string UserId { get; set; } = default!;
}
