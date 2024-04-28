using HospitalMangement.Domain.Models.Authentication;
using HospitalMangement.Domain.Models.Entity;
using HospitalMangment.Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalMangementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientLoginController : ControllerBase
    {
        private readonly HospitaldbContext _context;

     public   PatientLoginController(HospitaldbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(PatientLoginModel model)
        {
            var patient = _context.Patients.SingleOrDefault(p => p.FirstName == model.firstName && p.DateOfBirth == model.DateOfBirth);
            if(patient == null)
            {
                return BadRequest("Invalid login credentials");
            }
            return Ok(new { patient.PatientId, patient.FirstName, patient.DateOfBirth });
        }


        [HttpGet]
        [Route("{firstName}/prescrptions")]
        public IActionResult GetAppointmentsAndPrescriptions(string firstName)
        {
            var patient = _context.Patients.SingleOrDefault(p => p.FirstName == firstName);

            if (patient == null)
            {
                return NotFound();
            }

            var appointments = _context.Appointments.Where(a => a.PatientId == patient.PatientId);

 
           var prescriptions = _context.Prescriptions.Where(p => p.Appointment.PatientId == patient.PatientId && p.PayementReceived)
                 .Select(a => new
                 {
                     appointmentId = a.AppointmentId,
                     patientFirstName = a.Appointment.Patient.FirstName,
                     patientLastName = a.Appointment.Patient.LastName,
                     patientDisease = a.Appointment.Diseases,
                     prescrptionDetails=a.PrescriptionDetails

                     

            }).ToList();

          


            // you can customize the response depending on your needs
            return Ok(new { prescriptions });
        }
    }



}

