using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMangement.Domain.Models.ServiceModel
{
    public class AppointmentByReceptionist
    {
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string DoctorName { get; set; }
        public string status { get; set; }
        public string Diseases { get; set; }
        public string ReceptionistName { get; set; }
        public int Fees { get; set; }
    }
}
