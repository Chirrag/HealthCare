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

    public  class DoctorRepository : IDoctorRepository
    {
     
            private readonly HospitaldbContext _context;

            public DoctorRepository(HospitaldbContext context)
            {
                _context = context;
            }

            //Get all doctor 
            public async Task<IEnumerable<Doctor>> GetAllDcotor()
            {
                return await _context.Doctors.ToListAsync();
            }

            // add doctor 
            public async Task AddDoctor(DoctorViewModel doctorViewModel)
            {
                var doctoradd = new Doctor();
                doctoradd.DoctorId = doctorViewModel.DoctorId;
                doctoradd.DoctorName = doctorViewModel.DoctorName;
                doctoradd.Department = doctorViewModel.Department;
                doctoradd.IsActive = doctorViewModel.IsActive;
                doctoradd.CreatedAt = DateTime.Now;

                _context.Doctors.Add(doctoradd);
                await _context.SaveChangesAsync();
            }

            // GetBy id 
            public async Task<Doctor> GetDoctorById(int id)
            {
                return await _context.Doctors.FindAsync(id);
            }



            public async Task UpdateDoctor(DoctorViewModel doctorViewModel)
            {
                var doctor = await _context.Doctors.FindAsync(doctorViewModel.DoctorId);
                if (doctor == null)
                {
                    throw new Exception("Patient Not Found");
                }
                doctor.DoctorName = doctorViewModel.DoctorName;
                doctor.Department = doctorViewModel.Department;
                doctor.IsActive = doctorViewModel.IsActive;
                doctor.UpdatedAt = DateTime.Now;
                _context.Update(doctor);

                await _context.SaveChangesAsync();
            }

            public async Task DeleteDoctor(int docid)
            {
                var doctor = await _context.Doctors.FindAsync(docid);
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();

            }
        }
}
