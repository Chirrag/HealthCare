import { Component, NgZone, OnInit } from '@angular/core';
import { AppointmentsComponent } from '../appointments/appointments.component';
import { AppointmentService } from '../shared/appointment.service';
import { CreatAppointment } from '../shared/creat-appointment';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { LoginService } from '../shared/login.service';

@Component({
  selector: 'app-prescrptions',
  templateUrl: './prescrptions.component.html',
  styleUrls: ['./prescrptions.component.css']
})
export class PrescrptionsComponent implements OnInit {
  
  appointment: any;
  patientAppointmentId:number;

  constructor(private  route:ActivatedRoute,
    private services:AppointmentService ,
    private form:FormBuilder,
     private _ngZone:NgZone,
     private router:Router,
     private tostr:ToastrService,
     private LoginService:LoginService
     )
     
     {
    this.route.params.subscribe(params => {
      this.patientAppointmentId = +params['id'];
      this.getstatus();
      this.PrescrptionForm();
  });
  }
  
  ngOnInit() {
    // const id= this.route.snapshot.paramMap['id'];
      this.services.getAppointmentById(this.patientAppointmentId).subscribe(res => {
        this.appointment = res;
        console.log(this.appointment);
      },
       err=>{
        this.router.navigateByUrl('/home-page');
       }
      );
      this.services.getPresceptionDetails();
    //this.OnLogoutClick();
  // Appointmentlist :any[];

  }


  status:any[]=[];

  getstatus(){
   return this.services.getStatus().subscribe(res=>{
     this.status=res;
     console.log(this.status);
   })
  }

    prescriptionFormPost : FormGroup;

    PrescrptionForm(){
      this.prescriptionFormPost=this.form.group({
        appointmentId:[this.patientAppointmentId],
        status:[''],
        prescriptionDetails:['']
      })
    }
    
    
    OnSubmit(){
      console.log(this.prescriptionFormPost.value);
      this.services.CreatePrescrption(this.prescriptionFormPost.value).subscribe(
        (res :any) =>{
          setTimeout(() => {
            this.tostr.success('Subbmitted Successfully ','Prescrptions Added', {timeOut:2000, closeButton:true});
            }, 200);
          this._ngZone.run(()=>this.router.navigateByUrl('/appointments'));
        
        });
      
    }
    
    OnLogoutClick(){
      this.LoginService.logoutUser();
      this.router.navigate(['/home-page']);
    }

}
