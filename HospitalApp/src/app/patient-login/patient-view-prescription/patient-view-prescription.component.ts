import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { PatientService } from 'src/app/shared/patient.service';

@Component({
  selector: 'app-patient-view-prescription',
  templateUrl: './patient-view-prescription.component.html',
  styleUrls: ['./patient-view-prescription.component.css']
})
export class PatientViewPrescriptionComponent {
    patientName:any;
    helper:any;
     
    constructor(private router:Router,
      private patient:PatientService
      
      ){
      this.helper=this.router.getCurrentNavigation().extras.state;
      this.patientName=this.helper.name;
      console.log(this.patientName);
    this.getAll();
    }
   prescrption:any = [];

    getAll(){
      this.patient.getPatientPrescrption(this.patientName).subscribe((res:any)=>{
        this.prescrption=res.prescriptions;
        console.log(this.prescrption.prescriptions);
      })
    }
        
}
