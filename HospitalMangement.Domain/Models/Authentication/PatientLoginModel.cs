using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMangement.Domain.Models.Authentication
{
    public  class PatientLoginModel
    {
        public string firstName { get; set; }

        public  DateTime DateOfBirth  { get; set; }
    }
}
