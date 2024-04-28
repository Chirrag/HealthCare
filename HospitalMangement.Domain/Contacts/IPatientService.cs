using HospitalMangement.Domain.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMangement.Domain.Contacts
{
    public  interface IPatientService
    { 
            Task<List<object>> GetById(int id);
             Task<List<PatientViewModel>> FillterPatient(int page,int pageSize);    

        }
}
