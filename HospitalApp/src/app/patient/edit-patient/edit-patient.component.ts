import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { AppointmentService } from 'src/app/shared/appointment.service';
import { LoginService } from 'src/app/shared/login.service';
import { Patient } from 'src/app/shared/patient';
import { PatientService } from 'src/app/shared/patient.service';

@Component({
  selector: 'app-edit-patient',
  templateUrl: './edit-patient.component.html',
  styleUrls: ['./edit-patient.component.css']
})
export class EditPatientComponent implements OnInit {

  PatientFormPost:FormGroup;
  data :Patient;

  constructor(private services:PatientService, 
    private route:ActivatedRoute,
    private fb :FormBuilder,
    private router:Router,
    private loginService:LoginService
    )
    {
      const id = this.route.snapshot.params['id'];
      console.log(id);
      this.services.getPatientById(id).subscribe(res=>{
       this.data=res;
        console.log(this.data);
        this.PatientFormPost=this.fb.group({
          patientId: [res.patientId, Validators.required],
            firstName: [res.firstName, Validators.required],
            lastName: [res.lastName, Validators.required],
            phoneNumber: [res.phoneNumber, Validators.required],
            dateOfBirth: [res.dateOfBirth, Validators.required],
            age: [res.age, Validators.required],
            address: [res.address, Validators.required],
            gender: [res.gender, Validators.required],
            isActive: [res.isActive, Validators.required]
        });
       
    },
    err=>{
      console.log("hey bro "+err)
      this.router.navigateByUrl('/home-page');
    });
    }


  ngOnInit(): void {
    this.PatientFormPost= this.fb.group({
      patientId: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      age: ['', Validators.required],
      address: ['', Validators.required],
      gender: ['', Validators.required],
      isActive: ['', Validators.required]
    })
}

Onsubmit(){
  const id = this.route.snapshot.params['id'];
    console.log(this.PatientFormPost.value);
    this.services.UpdatePatient(id, this.PatientFormPost.value).subscribe((res) => {
      console.log(res);
         console.log('Post updated successfully!');
         this.router.navigateByUrl('patient');
    }
    )
}
OnLogoutClick(){
  this.loginService.logoutUser();
  this.router.navigate(['/home-page']);
}



}