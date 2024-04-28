using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMangement.Domain.Models.ServiceModel
{
    public class AppointmentPrescrption
    {
        public int PrescriptionId { get; set; }
        public int AppointmentId { get; set; }
        public string PrescriptionDetails { get; set; } = null!;
        public string status { get; set; }
        public bool IsActive { get; set; }



    }
}
