using Campany.BL.Dots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campany.BL;

public interface ITicketManager
{

    List<TicketReadDto> GetAll();
    TicketReadDto? GetById(int id);
    int Add(TicketAddDto ticketAddDto);
    bool Update(TicketUpdateDto ticketUpdateDto);
    void Delete(int id);
    DepartmantDto? GetDetails(int id);
}
