using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campany.BL.Dots;


public class RegisterResultDto
{
    public RegisterResultDto(bool isSuccess, IEnumerable<IdentityError>? errors=null)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public bool IsSuccess { get; set; }
    public IEnumerable<IdentityError>? Errors { get; set; }
}
