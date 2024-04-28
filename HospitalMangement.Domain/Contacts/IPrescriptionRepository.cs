using HospitalMangement.Domain.Models.Entity;
using HospitalMangement.Domain.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMangment.Domain.Contacts
{
    public  interface IPrescriptionRepository 
    {
        Task<IEnumerable<Prescription>> GetAll();
        Task AddPrescription(PrescriptionViewModel prescriptionView);
        Task<Prescription> GetPrecById(int id);
        Task UpdatePrecription(PrescriptionViewModel prescriptionView);
        Task DeletePrescription(int pecpid);
    }
}
