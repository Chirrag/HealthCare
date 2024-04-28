using HospitalMangement.Domain.Contacts;
using HospitalMangement.Domain.Models.Entity;
using HospitalMangement.Domain.Models.ViewModel;
using HospitalMangment.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMangment.Infrastructure.Repository
{
    public class AppointmentStatusRepository : IAppointmentStatusRepository
    {
        private readonly HospitaldbContext _context;

        public AppointmentStatusRepository(HospitaldbContext context)
        {
            _context = context;

        }

        public async Task<IEnumerable<AppointmentStatus>> GetAllAp()
        {
            return await _context.AppointmentStatuses.ToListAsync();
        }

        public async Task<AppointmentStatus> GetApById(int apid)
        {
            return await _context.AppointmentStatuses.FindAsync(apid);
        }
        public async Task AddAppointment(AppointmentStatusViewModel appointmentView)
        {
            var appointadd = new AppointmentStatus();
           appointadd.StatusId= appointmentView.StatusId;
            appointadd.Status = appointmentView.Status;

            appointadd.CreatedAt = DateTime.Now;

            _context.AppointmentStatuses.Add(appointadd);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateApoointment(AppointmentStatusViewModel appointmentView)
        {
            var updateUp = await _context.AppointmentStatuses.FindAsync(appointmentView.StatusId);
            if (updateUp == null)
            {
                throw new Exception("Patient Not Found");
            }
            updateUp.Status= appointmentView.Status;
            updateUp.UpdatedAt = DateTime.Now;

            _context.Update(updateUp);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAppointment(int aptid)
        {
            var appointment = await _context.AppointmentStatuses.FindAsync(aptid);
            _context.AppointmentStatuses.Remove(appointment);
            await _context.SaveChangesAsync();

        }
    }
}
