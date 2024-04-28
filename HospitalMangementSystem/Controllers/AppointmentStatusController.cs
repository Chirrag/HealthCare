using HospitalMangement.Domain.Contacts;
using HospitalMangement.Domain.Models.Entity;
using HospitalMangement.Domain.Models.ViewModel;
using HospitalMangment.Domain.Contacts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HospitalMangementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentStatusController : ControllerBase
    {
        private readonly IAppointmentStatusRepository _appointmentRepository;

        public AppointmentStatusController(IAppointmentStatusRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }


        // GET: api/<AppointmentController>
        [Authorize(Roles = "Receptionist,Doctor")]
        [HttpGet]
        public async Task<IEnumerable<AppointmentStatus>> Get()
        {
            return await _appointmentRepository.GetAllAp();
        }

        // GET api/<AppointmentController>/5
        [Authorize(Roles = "Receptionist,Doctor")]
        [HttpGet("{id}")]
        public async Task<AppointmentStatus> Get(int id)
        {
            return await _appointmentRepository.GetApById(id);
        }

        // POST api/<EmployeeController>
        [Authorize(Roles = "Receptionist,Doctor")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AppointmentStatusViewModel appointmentView)
        {
            if (appointmentView == null)
            {
                return BadRequest();
            }
            await _appointmentRepository.AddAppointment(appointmentView);
            return Ok();
        }



        // PUT api/<DoctorController>/5
        [Authorize(Roles = "Receptionist,Doctor")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AppointmentStatusViewModel appointmentView)
        {
            if (id != appointmentView.StatusId)
            {

                return BadRequest();
            }
            await _appointmentRepository.UpdateApoointment(appointmentView);
            return Ok("Successfully added");
        }

        //delete by id 
        [Authorize(Roles = "Receptionist,Doctor")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var recep = await _appointmentRepository.GetApById(id);
            if (recep == null)
            {
                return NotFound();
            }
            await _appointmentRepository.DeleteAppointment(id);
            return NoContent();
        }
    }
}
