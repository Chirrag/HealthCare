import { Component, NgZone, OnInit } from '@angular/core';
import { Form, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { from } from 'rxjs';
import { AppointmentService } from 'src/app/shared/appointment.service';
import { Prescription } from 'src/app/shared/prescription';
import jwt_decode from "jwt-decode";
import {ToastrService} from "ngx-toastr";
import { LoginService } from 'src/app/shared/login.service';

@Component({
  selector: 'app-edit-prescption',
  templateUrl: './edit-prescption.component.html',
  styleUrls: ['./edit-prescption.component.css']
})
export class EditPrescptionComponent implements OnInit {


  prep: Prescription;
  prescriptionForm: FormGroup;

  constructor(
    private fb :FormBuilder,
    private Service: AppointmentService,
    private router: Router,
    private route: ActivatedRoute,
    private Toastr:ToastrService,
    private _ngZone:NgZone,
    private LoginService: LoginService
    
  ) {
    const id = this.route.snapshot.params['id'];
    console.log(id);
    this.Service.getSinglePrescription(id).subscribe(res=>{
     this.prep=res;
     console.log(this.prep);
     this.prescriptionForm = this.fb.group({
      prescriptionId:[res.prescriptionId],
      appointmentId: [res.appointmentId],
      prescriptionDetails: [res.prescriptionDetails],
      payementReceived: [res.payementReceived],
      status:[res.status]
    });
  });
  }

   
  ngOnInit(): void {
 
    this.prescriptionForm = this.fb.group({
      prescriptionId:[''],
      appointmentId: [''],
      prescriptionDetails: [''],
      payementReceived: [''],
      status:["completed"]
    })
  
  }
  OnLogoutClick(){
    this.LoginService.logoutUser();
    this.router.navigate(['/home-page']);
  }
 
    // this.prescriptionForm = new FormGroup({
    //   appointmentId:new FormControl(''),
    //   prescriptionDetails:new FormControl(''),
    //   payementReceived:new FormControl(''),
    //   status:new FormControl("completed")
    // });

    
  
  Onsubmit(){
    const id = this.route.snapshot.params['id'];
      console.log(this.prescriptionForm.value);
      this.Service.updatePrescription(id, this.prescriptionForm.value).subscribe(
        (res:any ) => {
        setTimeout(() => {
          this.Toastr.info('updated  Successfully ','Update Payment State', {timeOut:2000, closeButton:true});
          }, 200);
        this._ngZone.run(()=>this.router.navigateByUrl('/prescrption-details'));
     })
       
  }
   
  // updatedata() {
  //   const updatePrescription={...this.prescription,...this.prescriptionForm.value};
  //   this.Service.updatePrescription(this.prescription.prescriptionId,updatePrescription).subscribe(( res =>{
  //     console.log(res);
  //   }))
  // }
}
