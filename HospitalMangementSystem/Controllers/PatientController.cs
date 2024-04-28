using HospitalMangement.Domain.Contacts;
using HospitalMangement.Domain.Models.Entity;
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
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;
        private readonly HospitaldbContext _context;
        private readonly IPatientService _patientService;

        public PatientController(IPatientRepository patientRepository,HospitaldbContext context,IPatientService patientService)

        {
            _context= context;
            _patientRepository = patientRepository;
            _patientService = patientService;
        }

        // GET: api/<PatientController>
        [HttpGet]
        [Authorize(Roles = "Receptionist")]
        public async Task<IEnumerable<Patient>> Get()
        {
            return await _patientRepository.GetAllPatient();
        }


        [HttpGet("ReceptionistGet/{id}")]
        [Authorize(Roles = "Receptionist")]
        public IActionResult GetById(int id)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.PatientId == id);

            if (patient == null)
            {
                return NotFound();
            }

            var patientViewModel = new PatientViewModel
            {
                PatientId = patient.PatientId,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                PhoneNumber = patient.PhoneNumber,
                DateOfBirth = patient.DateOfBirth,
                Age = patient.Age,
                Address = patient.Address,
                Gender = patient.Gender,
                IsActive = patient.IsActive
            };

            return Ok(patientViewModel);
        }

       

        // POST api/<EmployeeController>
        [HttpPost]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> Post([FromBody] PatientViewModel patient)
        {
            if (patient == null)
            {
                return BadRequest();
            }
            await _patientRepository.AddPatient(patient);
            return Ok(patient);
        }
        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> Put(int id, [FromBody] PatientViewModel patientViewModel)
        {
            if (id != patientViewModel.PatientId)
            {

                return BadRequest();
            }
            await _patientRepository.UpdatePatient(patientViewModel);
            return Ok();
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _patientRepository.GetPatientById(id);
            if (employee == null)
            {
                return NotFound();
            }
            await _patientRepository.DeletePatient(id);
            return NoContent();
        }


        // Filter Method 
        [HttpGet("PatientFilter")]
        public async Task<IActionResult> GetPatientsFilter([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var patient = await _patientService.FillterPatient(page, pageSize);
                return Ok(patient);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internet server error:{ex.Message}");
            }
        }


    }
}
