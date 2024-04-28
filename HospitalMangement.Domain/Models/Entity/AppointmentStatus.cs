using System;
using System.Collections.Generic;

namespace HospitalMangement.Domain.Models.Entity;

public partial class AppointmentStatus
{
    public int StatusId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
