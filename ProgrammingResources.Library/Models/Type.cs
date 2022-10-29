using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingResources.Library.Models;
public class Type
{
    public int TypeId { get; set; }
    public string Name { get; set; } = default!;
    public DateTime DateAdded { get; set; }
    public DateTime? DateDeleted { get; set; }
    public string UserId { get; set; } = default!;
}
