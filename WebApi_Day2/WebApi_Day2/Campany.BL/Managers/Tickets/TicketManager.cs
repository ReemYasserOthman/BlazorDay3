using Campany.BL.Dots;
using Campany.BL.Dots.Tickets;
using Campany.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Campany.BL;

public class TicketManager : ITicketManager
{
    private readonly ITicketRepo _ticketRepo;
    public TicketManager(ITicketRepo ticketRepo)
    {
        _ticketRepo= ticketRepo;
    }

    public int Add(TicketAddDto ticketAddDto)
    {
        var ticket = new Ticket
        {
            Name = ticketAddDto.Name,
            Description = ticketAddDto.Description,
            DepartmentId = ticketAddDto.DepartmentId,
        };
        _ticketRepo.Add(ticket);
        _ticketRepo.SaveChanges();
        return ticket.Id;
    }

    public void Delete(int id)
    {
        var ticketfromdb= _ticketRepo.GetById(id);
        if (ticketfromdb == null) { return; }
        _ticketRepo.Delete(ticketfromdb);
        _ticketRepo.SaveChanges();
    }

    public List<TicketReadDto> GetAll()
    {
       IEnumerable<Ticket> ticketfromdb=_ticketRepo.GetAll();
        return ticketfromdb.Select(d=> new TicketReadDto
        {
            Id = d.Id,
            Name = d.Name,
            Description= d.Description,
            DepartmentId=d.DepartmentId,
        }).ToList();
    }

    public TicketReadDto? GetById(int id)
    {
        var ticket =_ticketRepo.GetById(id);
        if(ticket == null) { return null; }
        return new TicketReadDto
        {
            Id = ticket.Id,
            Name = ticket.Name,
            Description = ticket.Description,
            DepartmentId = ticket.DepartmentId,
        };
    }

    public bool Update(TicketUpdateDto ticketUpdateDto)
    {
        var ticketfromdb = _ticketRepo.GetById(ticketUpdateDto.Id);
        if (ticketfromdb == null) { return false; }
        ticketfromdb.Description = ticketUpdateDto.Description;
        ticketfromdb.Name= ticketUpdateDto.Name;
        ticketfromdb.DepartmentId = ticketUpdateDto.DepartmentId;
        _ticketRepo.Update(ticketfromdb);
        _ticketRepo.SaveChanges();
        return true;
    }
    public DepartmantDto? GetDetails(int id)
    {
        Department? dept = _ticketRepo.GetWithdeptsAndtickets(id);
        if (dept is null)
        {
            return null;
        }

        return new DepartmantDto
        {
            Id = dept.Id,
            Name = dept.Name,
             


            
            Tickets = dept.tickets
                .Select(d => new TicketReadDetailDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description= d.Description,
                   
                    DevelopersCount =d.Developers.Count
                }).ToList()
        };
    }
}
