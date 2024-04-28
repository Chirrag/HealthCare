
using HospitalMangement.Domain.Models.Entity;
using HospitalMangement.Domain.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMangment.Domain.Contacts
{
    public  interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllPatient();
        Task<Patient> GetPatientById(int id);
        Task<IEnumerable<Patient>> GetActivePatient();
        Task AddPatient(PatientViewModel patientViewModel);
        Task UpdatePatient(PatientViewModel patientViewModel);
        Task DeletePatient(int patid);

    }
}
