using HospitalMangement.Domain.Models.Entity;
using HospitalMangement.Domain.Models.ServiceModel;
using HospitalMangement.Domain.Models.ViewModel;
using HospitalMangment.Domain.Contacts;
using HospitalMangment.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HospitalMangementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionRepository _repository;
        private readonly HospitaldbContext _context;

        public PrescriptionController(IPrescriptionRepository repository,HospitaldbContext context)
        {
            _repository = repository;
            _context = context;
        }


        // GET: api/<PrescriptionController>
        [Authorize(Roles = "Receptionist,Doctor")]
        [HttpGet]
       
        public async Task<IEnumerable<Prescription>> Get()
        {
            return await _repository.GetAll();
        
        }

        [Authorize(Roles = "Receptionist,Doctor")]
        [HttpGet("{id}")]
       
        public async Task<Prescription> Get(int id)
        {
            return await _repository.GetPrecById(id);
        }

        [Authorize(Roles = "Receptionist,Doctor")]
        [HttpPost("CreatePrescrption")]
        public async Task<IActionResult> CreatePrescription(AppointmentPrescrption prescription)
        {
            // Generate a new prescription ID
            var Doctor = await _context.Doctors.FirstOrDefaultAsync(r => r.DoctorName == User.Identity.Name);
            var appointment = _context.Appointments.FirstOrDefault(r => r.AppointmentId == prescription.AppointmentId);
            var status = _context.AppointmentStatuses.FirstOrDefault(r => r.Status == prescription.status);

            var prescrptions = new Prescription
            {
                AppointmentId = appointment.AppointmentId,
                PrescriptionDetails = prescription.PrescriptionDetails,
                CreatedAt = DateTime.Now
            };
            appointment.StatusId = status.StatusId;

/*            _context.Appointments.Update(appointment);*/

            _context.Prescriptions.Add(prescrptions);
            await _context.SaveChangesAsync();

            return Ok();
        }


        // POST api/<PrescriptionController>
        [HttpPost]
        [Authorize(Roles = "Receptionist,Doctor")]
        public async Task<IActionResult> Post([FromBody] PrescriptionViewModel prescriptionView)
        {
            if (prescriptionView == null)
            {
                return BadRequest();
            }
            await _repository.AddPrescription(prescriptionView);
            return Ok();
        }


        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Receptionist,Doctor")]
        public async Task<IActionResult> Put(int id, [FromBody] PrescriptionViewModel prescriptionView)
        {
            if (id != prescriptionView.PrescriptionId)
            {

                return BadRequest();
            }
            await _repository.UpdatePrecription(prescriptionView);
            return Ok();
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Receptionist,Doctor")]
        public async Task <IActionResult> Patch(int id, PrescriptionViewModel prescriptionView)
        {
            if(id != prescriptionView.PrescriptionId){
                return BadRequest();
            }
            await _repository.UpdatePrecription(prescriptionView);
            return Ok("Successfully Added");
        }




        [HttpDelete("{id}")]
        [Authorize(Roles = "Receptionist,Doctor")]
        public async Task<IActionResult> Delete(int id)
        {
            var prec = await _repository.GetPrecById(id);
            if (prec == null)
            {
                return NotFound();
            }
            await _repository.GetPrecById(id);
            return NoContent();
        }
    }
}
