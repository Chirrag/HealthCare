using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMangement.Domain.Models.ViewModel
{
    public class PrescriptionViewModel
    {
        public int PrescriptionId { get; set; }
        public int AppointmentId { get; set; }
        public string PrescriptionDetails { get; set; } = null!;
        public bool PayementReceived { get; set; }
        public string? status { get; set; }
    }
}
