using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campany.DAL;

public class Ticket
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
    public Department? department { get; set; }
    public ICollection<Developer> Developers { get; set; }=new HashSet<Developer>();
    
}
