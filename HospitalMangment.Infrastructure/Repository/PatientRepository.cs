
using HospitalMangement.Domain.Models.Entity;
using HospitalMangement.Domain.Models.ViewModel;
using HospitalMangment.Domain.Contacts;
using HospitalMangment.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMangement.Infrastructure.Repository
{

    public class PatientRepository : IPatientRepository
    {
        private readonly HospitaldbContext _context;

        public PatientRepository(HospitaldbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Patient>> GetActivePatient()
        {
            return await _context.Patients.Where(a => a.IsActive).ToListAsync();
        }




        // Get all Patient 
        public async Task<IEnumerable<Patient>> GetAllPatient()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task AddPatient(PatientViewModel patientViewModel)
        {
            var patientAdd = new Patient();
            patientAdd.PatientId = patientViewModel.PatientId;
            patientAdd.FirstName = patientViewModel.FirstName;
            patientAdd.LastName = patientViewModel.LastName;
            patientAdd.PhoneNumber = patientViewModel.PhoneNumber;
            patientAdd.DateOfBirth = patientViewModel.DateOfBirth;
            patientAdd.Age = patientViewModel.Age;
            patientAdd.Address = patientViewModel.Address;
            patientAdd.Gender = patientViewModel.Gender;
            patientAdd.IsActive = patientViewModel.IsActive;
            patientAdd.CreatedAt = DateTime.Now;

            _context.Patients.Add(patientAdd);
            await _context.SaveChangesAsync();
        }
        // Get All Patients 
        public async Task<Patient> GetPatientById(int id)
        {
            return await _context.Patients.FindAsync(id);
        }

       



        // Put All Patients
        public async Task UpdatePatient(PatientViewModel patientViewModel)
        {
            var patientUp = await _context.Patients.FindAsync(patientViewModel.PatientId);
            if (patientUp == null)
            {
                throw new Exception("Patient Not Found");
            }
            patientUp.FirstName = patientViewModel.FirstName;
            patientUp.LastName = patientViewModel.LastName;
            patientUp.PhoneNumber = patientViewModel.PhoneNumber;
            patientUp.DateOfBirth = patientUp.DateOfBirth;
            patientUp.Age = patientViewModel.Age;
            patientUp.Address = patientViewModel.Address;
            patientUp.Gender = patientViewModel.Gender;
            patientUp.IsActive = patientViewModel.IsActive;
            patientUp.UpdatedAt = DateTime.Now;
            _context.Update(patientUp);

            await _context.SaveChangesAsync();
        }
        public async Task DeletePatient(int patid)
        {
            var patient = await _context.Patients.FindAsync(patid);
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        }
    }
}
