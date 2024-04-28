using HospitalMangement.Domain.Models.Entity;
using HospitalMangement.Domain.Models.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMangement.Domain.Contacts
{
    public interface IAppointmentService
    {
        Task<List<object>> GetDoctorAppointments(string doctorName);
        Task<List<object>> GetAllAppointments();
        Task<bool> UpdateAppointment(int appointmentId);
        Task<object> GetDoctorAppointmentById(int appointmentId, string doctorName);
        Task CreateAppointment(AppointmentByReceptionist model, string receptionistName);
    }
}
