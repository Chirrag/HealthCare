using HospitalMangement.Domain.Models.Entity;
using HospitalMangement.Domain.Models.ViewModel;
using HospitalMangment.Domain.Contacts;

using HospitalMangment.Infrastructure.Context;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMangement.Infrastructure.Repository
{
    public  class AppointmentRepository : IAppointmentRepository
    {
        private readonly HospitaldbContext _context;
        
        public AppointmentRepository(HospitaldbContext context)
        {
            _context = context;

        }

        public async Task<IEnumerable<Appointment>> GetAllAp()
        {
            return await _context.Appointments.ToListAsync();
        }
        // Get All Active Appointment using ISActive
        public async Task<IEnumerable<Appointment>> GetActiveAp()
        {
            return await _context.Appointments.Where(a=>a.IsActive).ToListAsync();
        }



        public async Task<Appointment> GetApById(int apid)
        {
            return await _context.Appointments.FindAsync(apid);
        }

        // API for UI see the Selecting the See Doctor All the Appointments
        
       /* public async Task<Appointment> GetAppointmentsByDoctor(int? doctorid)
        {
            var appointments=_context.Appointments.Include(a=>a.Patient).Include(a=>a.Doctor);
            if(doctorid.Hasvalue)
            {
                appointments=appointments.where(a=>)
            }
        }
*/

        public async Task AddAppointment(AppointmentViewModel appointmentView)
        {
            var appointadd = new Appointment();
            appointadd.AppointmentId = appointmentView.AppointmentId;
            appointadd.PatientId = appointmentView.PaitentId;
            appointadd.DoctorId = appointmentView.DoctorId;
            appointadd.StatusId = appointmentView.StatusId;
            appointadd.ReceptionistId = appointmentView.ReceptionistId;
            appointadd.Diseases = appointmentView.Diseases;
            appointadd.Fees = appointmentView.Fees;
            
            appointadd.IsActive = true;

            appointadd.CreatedAt = DateTime.Now;

            _context.Appointments.Add(appointadd);
            await _context.SaveChangesAsync();
        }


     

        public async Task UpdateApoointment(AppointmentViewModel appointmentView)
        {
            var updateUp = await _context.Appointments.FindAsync(appointmentView.AppointmentId);
            if (updateUp == null)
            {
                throw new Exception("Patient Not Found");
            }
            updateUp.PatientId = appointmentView.PaitentId;
            updateUp.DoctorId = appointmentView.DoctorId;
            updateUp.StatusId = appointmentView.StatusId;
            updateUp.ReceptionistId = appointmentView.ReceptionistId;
            updateUp.Diseases = appointmentView.Diseases;
            updateUp.Fees = appointmentView.Fees;
            updateUp.IsActive = appointmentView.IsActive;
            updateUp.UpdateAt = DateTime.Now;
            

            _context.Update(updateUp);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAppointment(int aptid)
        {
            var appointment = await _context.Appointments.FindAsync(aptid);
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

        }

    }
}
