using System;
using System.Collections.Generic;

namespace HospitalMangement.Domain.Models.Entity
{
    public partial class Appointment
    {
        public Appointment()
        {
            Prescriptions = new HashSet<Prescription>();
        }

        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int ReceptionistId { get; set; }
        public int StatusId { get; set; }
        public string Diseases { get; set; } = null!;
        public int Fees { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        public virtual Doctor Doctor { get; set; } = null!;
        public virtual Patient Patient { get; set; } = null!;
        public virtual Receptionist Receptionist { get; set; } = null!;
        public virtual AppointmentStatus Status { get; set; } = null!;
        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}
