
using HospitalMangement.Domain.Contacts;
using HospitalMangement.Domain.Models.Entity;
using HospitalMangement.Domain.Models.ServiceModel;
using HospitalMangment.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Core.UseCase.Appointments
{
    public  class AppointmentService : IAppointmentService

    {
        private readonly HospitaldbContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public AppointmentService(HospitaldbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<object>> GetDoctorAppointments(string doctorName)
        {
            /*var user = await _userManager.FindByNameAsync(User.Identity.Name);*/

            var appointments = await _context.Appointments
                .Where(a => a.Doctor.DoctorName == doctorName && a.IsActive)
                .Select(a => new
                {
                    PatientAppointmentId = a.AppointmentId,
                    PatientFirstName = a.Patient.FirstName,
                    PatientLastName = a.Patient.LastName,
                    PatientDoctorName = a.Doctor.DoctorName,
                    PatientDiesases = a.Diseases,
                    appointmentstatus = a.Status.Status
                })
                .ToListAsync();

             return appointments.Cast<object>().ToList();
        }

        public async Task<List<object>> GetAllAppointments()
        {
            var appointments = await _context.Appointments
                .Select(a => new
                {
                    PatientAppointmentId = a.AppointmentId,
                    PatientFirstName = a.Patient.FirstName,
                    PatientLastName = a.Patient.LastName,
                    PatientDoctorName = a.Doctor.DoctorName,
                    PatientDiesases = a.Diseases,
                    appointmentstatus = a.Status.Status,
                    isActive = a.IsActive
                }).ToListAsync();

            return appointments.Cast<object>().ToList();
        }

        public async Task<bool> UpdateAppointment(int appointmentId)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);

            if (appointment == null)
            {
                return false;
            }

            appointment.IsActive = !appointment.IsActive;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<object> GetDoctorAppointmentById(int appointmentId, string doctorName)
        {
            var appointment = await _context.Appointments
                .Where(a => a.AppointmentId == appointmentId && a.Doctor.DoctorName == doctorName)
                .Select(a => new
                {
                    PatientAppointmentId = a.AppointmentId,
                    PatientFirstName = a.Patient.FirstName,
                    PatientLastName = a.Patient.LastName,
                    PatientDoctorName = a.Doctor.DoctorName,
                    PatientDiseases = a.Diseases,
                    AppointmentStatus = a.Status.Status
                })
                .FirstOrDefaultAsync();

            return appointment;
        }

        // Create Appointment Services

        public async Task CreateAppointment(AppointmentByReceptionist model, string receptionistName)
        {
            var receptionist = await _context.Receptionists.FirstOrDefaultAsync(r => r.ReceptionistName == receptionistName);
            var patient = _context.Patients.FirstOrDefault(p => p.FirstName == model.PatientFirstName && p.LastName == model.PatientLastName);
            var doctor = _context.Doctors.FirstOrDefault(d => d.DoctorName == model.DoctorName);
            var status = _context.AppointmentStatuses.FirstOrDefault(s => s.Status == model.status);

            if (patient == null)
            {
                throw new Exception("Patient not found");
            }

            if (doctor == null)
            {
                throw new Exception("Doctor not found");
            }

            if (status == null)
            {
                throw new Exception("Status not found");
            }

            var appointment = new Appointment
            {
                PatientId = patient.PatientId,
                DoctorId = doctor.DoctorId,
                StatusId = status.StatusId,
                ReceptionistId = receptionist.ReceptionistId,
                IsActive = true,
                Diseases = model.Diseases,
                Fees = model.Fees,
                CreatedAt = DateTime.UtcNow
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
        }

    }
}
