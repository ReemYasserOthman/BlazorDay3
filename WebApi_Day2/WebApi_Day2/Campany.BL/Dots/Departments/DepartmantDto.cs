using Campany.BL.Dots.Tickets;
using Campany.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campany.BL.Dots;

public class DepartmantDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public ICollection<TicketReadDetailDto> Tickets { get; set; } = new HashSet<TicketReadDetailDto>();
}


