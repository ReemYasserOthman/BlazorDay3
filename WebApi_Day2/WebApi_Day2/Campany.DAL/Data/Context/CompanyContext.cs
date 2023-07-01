using Campany.DAL.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Campany.DAL;

public class CompanyContext :IdentityDbContext<Employee>
{
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<Developer> Developers => Set<Developer>();
    public DbSet<Department> Departments => Set<Department>();

    public CompanyContext(DbContextOptions<CompanyContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // call base 3shan ana 3eza al parent code m3ya 
        var tickets = new List<Ticket>
        {
             new Ticket {
                    Id= 1,
                    Name= "ticket1",
                    Description= "Hematology",
                    DepartmentId= 1,
                    
             },
                  new Ticket {
                    Id= 2,
                    Name= "ticket2",
                    Description= "Neurology",
                    DepartmentId= 2,
                    
                  },
                  new Ticket {
                    Id= 3,
                    Name= "ticket3",
                    Description= "Pediatrics",
                    DepartmentId= 3,
                   
                  },
                  new Ticket {
                    Id= 4,
                    Name= "ticket4",
                    Description= "Hematology",
                    DepartmentId= 4,
                    
                  }
        };
        var departments = new List<Department>
        {
             new Department {
                    Id= 1,
                    Name= "dept 1"
                    
                  },
                  new Department {
                    Id= 2,
                    Name="dept 2"
                   
                  },
                  new Department {
                    Id= 3,
                    Name= "dept 3"
                    
                  },
                  new Department {
                    Id= 4,
                    Name= "dept 4"
                    
                  }
        };
        var developers = new List<Developer> {

          new Developer { Id= 1, Name= "Dana" },
                  new Developer { Id= 2, Name= "Isaac" },
                  new Developer { Id= 3, Name= "Damon" },
                  new Developer { Id= 4, Name= "Miriam",},
                  new Developer { Id= 5, Name= "Terence" },
                  new Developer { Id= 6, Name= "Roosevelt" },
                  new Developer { Id= 7, Name= "Eduardo" },
                  new Developer { Id= 8, Name= "Wilbert" },
                  new Developer { Id= 9, Name= "Tasha" },
                  new Developer { Id= 10, Name= "Max",},
                  new Developer { Id= 11, Name= "Bridget" }

        };
        modelBuilder.Entity<Ticket>().HasData(tickets);
        modelBuilder.Entity<Developer>().HasData(developers);
        modelBuilder.Entity<Department>().HasData(departments);



    }

}
