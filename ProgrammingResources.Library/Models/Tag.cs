using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingResources.Library.Models;
public class Tag
{
    public int TagId { get; set; }
    public string Name { get; set; } = default!;
    public DateTime DateCreated { get; set; }
    public DateTime? DateDeleted { get; set; }
    public string UserId { get; set; } = default!;
}
