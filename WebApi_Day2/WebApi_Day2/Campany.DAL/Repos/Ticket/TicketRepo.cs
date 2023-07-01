using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Campany.DAL;

public class TicketRepo : ITicketRepo
{
    private readonly CompanyContext _context;
    public TicketRepo(CompanyContext context)
    {
        _context = context;
    }

    public void Add(Ticket ticket)
    {
       _context.Set<Ticket>().Add(ticket);
    }

    public void Delete(Ticket ticket)
    {
        _context.Set<Ticket>().Remove(ticket);
    }

    public IEnumerable<Ticket> GetAll()
    {
        return _context.Set<Ticket>().AsNoTracking();
    }

    public Ticket? GetById(int id)
    {
        return _context.Set<Ticket>().Find(id);
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    public void Update(Ticket ticket)
    {
        
    }

    public Department? GetWithdeptsAndtickets(int id)
    {
        return _context.Departments
            .Include(d => d.tickets).
            ThenInclude(p => p.Developers).
            FirstOrDefault(d => d.Id == id);
    }
}
