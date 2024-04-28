using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace HospitalMangement.Domain.Models.Entity;

public partial class Receptionist
{
    public int ReceptionistId { get; set; }

    public string ReceptionistName { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
    /* public string IdentityUserId { get; set; }

     public IdentityUser IdentityUser { get; set; }
 */


    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
