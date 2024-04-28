using HospitalMangement.Domain.Contacts;
using HospitalMangement.Domain.Models.Authentication;
using HospitalMangement.Domain.Models.Entity;
using HospitalMangement.Domain.Models.ServiceModel;
using HospitalMangement.Domain.Models.ViewModel;
using HospitalMangment.Domain.Contacts;

using HospitalMangment.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HospitalMangementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository _appointmentRepository;
        /*private readonly HospitaldbContext _hospitaldb;*/
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAppointmentService _appointmentService;


        public AppointmentController(UserManager<IdentityUser> userManager, HospitaldbContext hospitaldb,IAppointmentRepository appointmentRepository,IAppointmentService appointmentService)
        {
            _userManager = userManager;
          /*  _hospitaldb= hospitaldb;*/
            _appointmentRepository = appointmentRepository;
            _appointmentService = appointmentService;
        }


        // GET: api/<AppointmentController>
        [Authorize(Roles = "Receptionist,Doctor")]
        [HttpGet]
          
        public async Task<IEnumerable<Appointment>> Get()
        {
            var isDoctor = User.IsInRole("Doctor");
           if(isDoctor)
            {
                return await _appointmentRepository.GetActiveAp();
            }
            else
            {
                return await _appointmentRepository.GetAllAp();
            }  
        }


      
        // Service of Doctor Appointment 
        [Authorize(Roles = "Doctor")]
        [HttpGet("DoctorAppointments")]
        public async Task<IActionResult> GetDoctorAppointments()
        {
            var doctorName = User.Identity.Name;
            var appointments = await _appointmentService.GetDoctorAppointments(doctorName);

            return Ok(appointments);
        }



        // Service 
        // All Appointment display get by
        [Authorize(Roles = "Receptionist")]
        [HttpGet("AllAppointments")]
        public async Task <IActionResult> GetAllAppointments()
        {

            var appointments = await _appointmentService.GetAllAppointments();

            return Ok(appointments);
        }


        //Service
        // Update Appointment where Receptionist click and the appointment will be Deactive
        [Authorize(Roles = "Receptionist")]
        [HttpGet("UpdateAppointment")]
        public async Task<IActionResult> UpdateAppointment(int appointmentId)
        {
            var updated = await _appointmentService.UpdateAppointment(appointmentId);

            if (!updated)
            {
                return BadRequest("Appointment not found");
            }

            return Ok();
        }

        // Get Doctor Appointment By  Doctor id and display his Data 
        [Authorize(Roles = "Doctor")]
        [HttpGet("DoctorAppointments/{appointmentId}")]
        public async Task<IActionResult> GetDoctorAppointmentById(int appointmentId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var appointment = await _appointmentService.GetDoctorAppointmentById(appointmentId, user.UserName);

            if (appointment == null)
            {
                return NotFound();
            }

            return Ok(appointment);
        }


        // Create Appointment from Receptionist Side and display his Portal 

        [Authorize(Roles = "Receptionist")]
        [HttpPost("CreateAppointment")]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentByReceptionist model)
        {
            try
            {
                var receptionistName = User.Identity.Name;
                await _appointmentService.CreateAppointment(model, receptionistName);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
   


            // GET api/<AppointmentController>/5
            [Authorize(Roles = "Receptionist,Doctor")]
        [HttpGet("{id}")]
        public async Task<Appointment> Get(int id)
        {
            return await _appointmentRepository.GetApById(id);
        }

        
        [Authorize(Roles = "Receptionist,Doctor")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AppointmentViewModel appointmentView)
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
        public async Task<IActionResult> Put(int id, [FromBody] AppointmentViewModel appointmentView)
        {
            if (id != appointmentView.AppointmentId)
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
