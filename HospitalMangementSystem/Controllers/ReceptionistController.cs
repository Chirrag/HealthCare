using HospitalMangement.Domain.Models.Entity;
using HospitalMangement.Domain.Models.ViewModel;
using HospitalMangment.Domain.Contacts;
using HospitalMangment.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HospitalMangementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceptionistController : ControllerBase
    {
        private readonly IReceptionistRepository _receptionistRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly HospitaldbContext _hospitaldb;

        public ReceptionistController(IReceptionistRepository receptionistRepository, UserManager<IdentityUser> userManager,HospitaldbContext hospitaldb)
        {
            _receptionistRepository = receptionistRepository;
            _userManager = userManager;
            _hospitaldb = hospitaldb;


        }



        // GET: api/<ReceptionistController>
        [HttpGet]
        public async Task<IEnumerable<Receptionist>> Get()
        {
            return await _receptionistRepository.GetAllRecp();
        }
        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public async Task<Receptionist> Get(int id)
        {
            return await _receptionistRepository.GetReceById(id);
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ReceptionistViewModel recepView)
        { 

            if (recepView == null)
            {
                return BadRequest();
            }
            await _receptionistRepository.AddRecep(recepView);
            return Ok();
        }
        // get api by id 
       
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ReceptionistViewModel recepViewModel)
        {
            if (id != recepViewModel.ReceptionistId)
            {

                return BadRequest();
            }
            await _receptionistRepository.UpdateRecep(recepViewModel);

            return Ok("Successfully added");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var recep = await _receptionistRepository.GetReceById(id);
            if (recep == null)
            {
                return NotFound();
            }
            await _receptionistRepository.GetReceById(id);
            return NoContent();
        }
    }
}
