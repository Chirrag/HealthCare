using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMangement.Domain.Models.ViewModel
{
    public class DoctorViewModel
    {
        public int DoctorId { get; set; }

        public string DoctorName { get; set; } = null!;

        public string Department { get; set; } = null!;

        public bool IsActive { get; set; }

    }
}
