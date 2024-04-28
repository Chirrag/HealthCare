using HospitalMangement.Domain.Models.Entity;
using HospitalMangement.Domain.Models.ViewModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMangment.Domain.Contacts
{
    public  interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAllAp();
        Task<IEnumerable<Appointment>> GetActiveAp();
     /*   Task<IEnumerable<Appointment>>GetAppointmentsByDoctor(int Doctorid);*/
        Task<Appointment> GetApById(int apid);
        Task AddAppointment(AppointmentViewModel appointmentView);
        Task UpdateApoointment(AppointmentViewModel appointmentView);
        Task DeleteAppointment(int apid);
    }
}
