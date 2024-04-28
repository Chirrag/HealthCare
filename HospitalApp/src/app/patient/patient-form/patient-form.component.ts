import { Component, NgZone, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginService } from 'src/app/shared/login.service';
import { PatientService } from 'src/app/shared/patient.service';
import { ToastrService } from 'ngx-toastr'; 

@Component({
  selector: 'app-patient-form',
  templateUrl: './patient-form.component.html',
  styleUrls: ['./patient-form.component.css']
})
export class PatientFormComponent implements OnInit {
    

  constructor (private PatientServcies: PatientService,
     private service:LoginService,
      private form:FormBuilder,
       private _ngZone:NgZone,
       private router:Router,
       private toastr:ToastrService
       ){

  }
  ngOnInit(): void {
    this.PatientForm();
  }
 
  PatientFormPost:FormGroup;

  PatientForm(){
    this.PatientFormPost=this.form.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      age: ['', Validators.required],
      address: ['', Validators.required],
      gender: ['', Validators.required],
      isActive: [true]
    })
  }
  OnSubmit(){
    console.log(this.PatientFormPost.value);
    //  this.PatientFormPost.value.isActive=="false"?false:true;
     this.PatientServcies.PatientData(this.PatientFormPost.value).subscribe(
      (res :any) =>{
        setTimeout(() => {
          this.toastr.success('Subbmitted Successfully ','Patient Added', {timeOut:2000, closeButton:true});
          }, 200);
        this._ngZone.run(()=>this.router.navigateByUrl('/patient'));
     })
    }

    OnLogoutClick(){
      this.service.logoutUser();
      this.router.navigate(['/home-page']);
    }
  }

