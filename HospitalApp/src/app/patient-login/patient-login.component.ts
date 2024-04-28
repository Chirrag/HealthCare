import { Component, OnInit } from '@angular/core';
import { LoginService } from '../shared/login.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-patient-login',
  templateUrl: './patient-login.component.html',
  styleUrls: ['./patient-login.component.css']
})
export class PatientLoginComponent implements OnInit {

  PatientLoginForm:FormGroup;

  constructor(
    private loginService : LoginService,
    private form : FormBuilder,
    private router:Router,
    ) {
    
    
  }

  ngOnInit(): void {
    this.PatientLoginForm=this.form.group({
      firstName:['',Validators.required],
      dateOfBirth:['',Validators.required]
  });
  }

  OnSubmit(){
  this.loginService.patientLogin(this.PatientLoginForm.value).subscribe(
   (res:any)=>{
    console.log("You login successfully");
    this.router.navigateByUrl('/patient-view-prescription',{state: { name: this.PatientLoginForm.value.firstName}});
   }    
  )
  }
}
