using HospitalMangement.Domain.Models.Entity;
using HospitalMangement.Domain.Models.ViewModel;
using HospitalMangment.Domain.Contacts;
using HospitalMangment.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMangement.Infrastructure.Repository
{

    public  class ReceptionistRepository : IReceptionistRepository
    {
        private readonly HospitaldbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ReceptionistRepository(HospitaldbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager= userManager;
            
        }

        public async Task<IEnumerable<Receptionist>> GetAllRecp()
        {
            return await _context.Receptionists.ToListAsync();
        }

        public async Task<Receptionist> GetReceById(int id)
        {
            return await _context.Receptionists.FindAsync(id);
        }

        public async Task AddRecep(ReceptionistViewModel recepViewModel)
        { 
            var RecepAdd = new Receptionist();
           
            RecepAdd.ReceptionistId = recepViewModel.ReceptionistId;
            RecepAdd.ReceptionistName = recepViewModel.ReceptionistName;
            RecepAdd.CreatedAt = DateTime.Now;

            _context.Receptionists.Add(RecepAdd);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRecep(ReceptionistViewModel recepViewModel)
        {
            var receup = await _context.Receptionists.FindAsync(recepViewModel.ReceptionistId);
            if (receup == null)
            {
                throw new Exception("Patient Not Found");
            }
            receup.ReceptionistName = recepViewModel.ReceptionistName;
            receup.UpdatedAt = DateTime.Now;
            _context.Update(receup);

            await _context.SaveChangesAsync();
        }
        public async Task DeleteRecep(int recpid)
        {
            var recep = await _context.Receptionists.FindAsync(recpid);
            _context.Receptionists.Remove(recep);
            await _context.SaveChangesAsync();

        }
    }
}
