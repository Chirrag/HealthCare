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

namespace HospitalManagement.Core.UseCase.Patient
{
    public  class PatientService : IPatientService
    {
        private readonly HospitaldbContext _context;
        public PatientService(HospitaldbContext context) 
        {
            _context = context;
        }

        public async Task<List<object>> GetById(int id)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.PatientId == id);

            if (patient == null)
            {
                throw new ArgumentException($"Patient with id {id} not found.");
            }

            var patientViewModel = new PatientViewModel
            {
                PatientId = patient.PatientId,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                PhoneNumber = patient.PhoneNumber,
                DateOfBirth = patient.DateOfBirth,
                Age = patient.Age,
                Address = patient.Address,
                Gender = patient.Gender,
                IsActive = patient.IsActive
            };

            var result = new List<object>();
            result.Add(patientViewModel);
            return result;
        }



        public async Task<List<PatientViewModel>> FillterPatient(int page, int pageSize)
        {
           
            var totalCount= await _context.Patients.CountAsync();
            var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
            var productPerPage = await _context.Patients
                .Skip((page-1) * pageSize)
                .Take(pageSize).ToListAsync();
            var patientViewModels = productPerPage.Select(patient => new PatientViewModel
            {
                PatientId = patient.PatientId,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                PhoneNumber = patient.PhoneNumber,
                DateOfBirth = patient.DateOfBirth,
                Age = patient.Age,
                Address = patient.Address,
                Gender = patient.Gender,
                IsActive = patient.IsActive
            }).ToList();

            return patientViewModels;
        }

    }
    }

