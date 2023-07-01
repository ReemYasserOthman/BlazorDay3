using Campany.BL.Dots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campany.BL.Managers;

public interface IAccManager
{
  Task<TokenDto> Login(LoginDto loginDto);
   Task< RegisterResultDto> Register(RegisterDto registerDto,string role);


}
