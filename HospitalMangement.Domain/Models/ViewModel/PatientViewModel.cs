using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMangement.Domain.Models.ViewModel
{
    public class PatientViewModel
    {
        public int PatientId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }

        public int Age { get; set; }

        public string Address { get; set; } = null!;

        public string Gender { get; set; } = null!;

        public bool IsActive { get; set; }

    }
}
