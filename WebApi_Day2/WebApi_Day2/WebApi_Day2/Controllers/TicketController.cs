using Campany.BL;
using Campany.BL.Dots;
using Campany.DAL;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_Day2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketManager _ticketManager;
        public TicketController(ITicketManager ticketManager)
        {
            _ticketManager = ticketManager;
        }
        [HttpGet]
        [Route("department/{id}")]
        public ActionResult<DepartmantDto> GetDetails(int id)
        {
            DepartmantDto? dept = _ticketManager.GetDetails(id);
            if (dept is null)
            {
                return NotFound();
            }
            return dept; //Status 200 Ok
        }

        [HttpGet]
        public ActionResult<List<TicketReadDto>> GetAll()
        {
            return _ticketManager.GetAll();  //Ok 200 //  
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult<TicketReadDto>GetById(int id)
        {
          // return new TicketReadDto { Id = id };
          var x= _ticketManager.GetById(id);
            if(x==null) { return NotFound(); }
            return Ok(x);
        }

        [HttpPut]
        public ActionResult Update(TicketUpdateDto ticketDto)
        {
            var isFound = _ticketManager.Update(ticketDto);
            if (!isFound)
            {
                return NotFound();
            }

            return NoContent(); //204 Success with not content
        }

        [HttpPost]
        public ActionResult Add(TicketAddDto ticketDto)
        {
            var newId = _ticketManager.Add(ticketDto);
            return CreatedAtAction  ("GetById",new { id = newId }, new {m= "ticket has been added successfully" });
            //return NoContent();
        }
        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            _ticketManager.Delete(id);
            return NoContent();
        }

    }
}
