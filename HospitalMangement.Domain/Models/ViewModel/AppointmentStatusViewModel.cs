using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMangement.Domain.Models.ViewModel
{
    public class AppointmentStatusViewModel
    {
        public int StatusId { get; set; }

        public string Status { get; set; } = null!;

    }
}
