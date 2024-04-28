using HospitalMangement.Domain.Models.Entity;
using HospitalMangement.Domain.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMangment.Domain.Contacts
{
    public  interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllDcotor();
        Task<Doctor> GetDoctorById(int id);
        Task AddDoctor(DoctorViewModel doctorViewModel);
        Task UpdateDoctor(DoctorViewModel doctorViewModel);
        Task DeleteDoctor(int docid);
    }
}
