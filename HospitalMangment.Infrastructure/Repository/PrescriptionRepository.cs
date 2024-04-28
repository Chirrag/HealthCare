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

    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly HospitaldbContext _context;

        public PrescriptionRepository(HospitaldbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Prescription>> GetAll()
        {
            return await _context.Prescriptions.ToListAsync();
        }

        public async Task<Prescription> GetPrecById(int id)
        {
            return await _context.Prescriptions.FindAsync(id);
        }

      

        public async Task AddPrescription(PrescriptionViewModel prescriptionView)
        {
            var prescrition = new Prescription();
            prescrition.PrescriptionId = prescriptionView.PrescriptionId;
            prescrition.AppointmentId = prescriptionView.AppointmentId;
            prescrition.PrescriptionDetails = prescriptionView.PrescriptionDetails;
            prescrition.PayementReceived = prescriptionView.PayementReceived;
            prescrition.CreatedAt = DateTime.Now;

            _context.Prescriptions.Add(prescrition);
            await _context.SaveChangesAsync();
        }


        public async Task UpdatePrecription(PrescriptionViewModel prescriptionView)
        {
            var prece = await _context.Prescriptions.FindAsync(prescriptionView.PrescriptionId);
            if (prece == null)
            {
                throw new Exception("Patient Not Found");
            }
            prece.AppointmentId = prescriptionView.AppointmentId;
            prece.PrescriptionDetails = prescriptionView.PrescriptionDetails;
            prece.PayementReceived = prescriptionView.PayementReceived;
            prece.Status = "Completed";
            prece.UpdatedAt = DateTime.Now;

            _context.Update(prece);

            await _context.SaveChangesAsync();
        }
        public async Task DeletePrescription(int pecpid)
        {
            var pec = await _context.Prescriptions.FindAsync(pecpid);
            _context.Prescriptions.Remove(pec);
            await _context.SaveChangesAsync();

        }
    }

}
