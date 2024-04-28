using System;
using System.Collections.Generic;

namespace HospitalMangement.Domain.Models.Entity
{
    public partial class Prescription
    {
        public int PrescriptionId { get; set; }
        public int AppointmentId { get; set; }
        public string PrescriptionDetails { get; set; } = null!;
        public bool PayementReceived { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Appointment Appointment { get; set; } = null!;
    }
}
