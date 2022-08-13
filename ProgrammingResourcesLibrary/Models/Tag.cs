using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingResourcesLibrary.Models;
public class Tag
{
    public int TagId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime DateCreated { get; set; }
}
