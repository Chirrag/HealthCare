using HospitalMangement.Domain.Models.Entity;
using HospitalMangement.Domain.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMangment.Domain.Contacts
{
    public  interface  IReceptionistRepository
    {
        Task<IEnumerable<Receptionist>> GetAllRecp();
        Task<Receptionist> GetReceById(int id);
        Task AddRecep(ReceptionistViewModel recepViewModel);
        Task UpdateRecep(ReceptionistViewModel recepViewModel);
        Task DeleteRecep(int recpid);
    }
}
