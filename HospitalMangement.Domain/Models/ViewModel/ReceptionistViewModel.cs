using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMangement.Domain.Models.ViewModel
{
    public class ReceptionistViewModel
    {
        public int ReceptionistId { get; set; }

        public string ReceptionistName { get; set; } = null!;

        /*  public string IdentityUserId { get; set; }

          public IdentityUser IdentityUser { get; set; }
  */
    }
}
