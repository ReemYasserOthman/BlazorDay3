using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campany.DAL.Data;

public class Employee :IdentityUser
{
    public string Department { get; set; } = string.Empty;

}
