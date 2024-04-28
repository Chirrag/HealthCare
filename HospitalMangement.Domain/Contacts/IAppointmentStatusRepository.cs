using HospitalMangement.Domain.Models.Entity;
using HospitalMangement.Domain.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMangement.Domain.Contacts
{
    public  interface IAppointmentStatusRepository
    {
        Task<IEnumerable<AppointmentStatus>> GetAllAp();
        Task<AppointmentStatus> GetApById(int apid);
        Task AddAppointment(AppointmentStatusViewModel appointmentView);
        Task UpdateApoointment(AppointmentStatusViewModel appointmentView);
        Task DeleteAppointment(int apid);
    }
}
