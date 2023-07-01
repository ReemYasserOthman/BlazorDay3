using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campany.DAL;

public interface ITicketRepo
{

    IEnumerable<Ticket> GetAll();
   Ticket? GetById(int id);
    void Add(Ticket ticket);
    void Update(Ticket ticket);
    void Delete(Ticket ticket);
    int SaveChanges();
    Department? GetWithdeptsAndtickets(int id);
}
