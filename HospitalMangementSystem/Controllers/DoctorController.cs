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
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorRepository _repository;

        public DoctorController(IDoctorRepository doctorRepository)
        {
            _repository = doctorRepository;
        }
        // GET: api/<PatientController>
       
        [HttpGet]
        [Authorize(Roles = "Receptionist")]
        public async Task<IEnumerable<Doctor>> Get()
        {
            return await _repository.GetAllDcotor();
        }
        // POST api/<EmployeeController>
        [HttpPost]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> Post([FromBody] DoctorViewModel doctor)
        {
            if (doctor == null)
            {
                return BadRequest();
            }
            await _repository.AddDoctor(doctor);
            return Ok("Successfully Added !!");
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Receptionist")]
        public async Task<Doctor> Get(int id)
        {
            return await _repository.GetDoctorById(id);
        }

        // PUT api/<DoctorController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> Put(int id, [FromBody] DoctorViewModel doctorViewModel)
        {
            if (id != doctorViewModel.DoctorId)
            {

                return BadRequest();
            }
            await _repository.UpdateDoctor(doctorViewModel);
            return Ok("Successfully Updated ");
        }

        // DELETE api/<DoctorController>/5
        //delete by id 
        [HttpDelete("{id}")]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> Delete(int id)
        {
            var recep = await _repository.GetDoctorById(id);
            if (recep == null)
            {
                return NotFound();
            }
            await _repository.DeleteDoctor(id);
            return NoContent();
        }
    }
}
