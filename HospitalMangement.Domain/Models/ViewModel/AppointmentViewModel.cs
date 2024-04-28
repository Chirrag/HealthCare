using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMangement.Domain.Models.ViewModel
{
    public class AppointmentViewModel
    {
        public int AppointmentId { get; set; }

        public int PaitentId { get; set; }

        public int DoctorId { get; set; }

        public int StatusId { get; set; }

        public int ReceptionistId { get; set; }

        public bool IsActive { get; set; }

        public string Diseases { get; set; } = null!;

        public int Fees { get; set; }
    }
}
