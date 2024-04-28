import { Component, NgZone, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Doctors } from 'src/app/shared/Doctors';
import { AppointmentService } from 'src/app/shared/appointment.service';
import { LoginService } from 'src/app/shared/login.service';
import jwt_decode from "jwt-decode";
import { ToastrService } from 'ngx-toastr';
import { PatientService } from 'src/app/shared/patient.service';
import { Patient } from 'src/app/shared/patient';
@Component({
  selector: 'app-appointments-form',
  templateUrl: './appointments-form.component.html',
  styleUrls: ['./appointments-form.component.css']
})
export class AppointmentsFormComponent implements OnInit{
  

  data :Patient;
  appointmentFormPost:FormGroup;
  constructor (private  serivces : AppointmentService,
    private loginService: LoginService, 
    private form:FormBuilder, 
    private _ngZone:NgZone,
    private router:Router,
    private toastr:ToastrService,
    private route:ActivatedRoute,
    private PatientService:PatientService
    )
    {
      const id = this.route.snapshot.params['id'];
       this.PatientService.getPatientById(id).subscribe(res =>{
        this.data=res;
        console.log(res);
  this.appointmentFormPost=this.form.group({
   
    patientFirstName: [res.firstName, Validators.required],
    patientLastName: [res.lastName, Validators.required],
    receptionistName: ['', Validators.required],
    diseases: ['', Validators.required],
    doctorName: ['', Validators.required],
    status: ['', Validators.required],
    fees: ['', Validators.required]
  })
})
   
    this.GetDoctor();
    this.getstatus();
    this.getRecep();
 }


 ngOnInit(): void {
   const id = this.route.snapshot.params['id'];
   console.log(id);
  var code= jwt_decode(localStorage.getItem("token"));
  var check =code["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
   if(check==="Receptionist"){
  this.router.navigate(['/appoinment-form',id]);
    } else{
       this.router.navigate(['/home-page']);
     }
  this.appointmentFormPost=this.form.group({
    patientFirstName: ['', Validators.required],
    patientLastName: ['', Validators.required],
    receptionistName: ['', Validators.required],
    diseases: ['', Validators.required],
    doctorName: ['', Validators.required],
    status: ['', Validators.required],
    fees: ['', Validators.required]
  },
  err=>{
    console.log("hey bro "+err)
    this.router.navigateByUrl('/loginForm');
  });
  



}
  Doctors: any[] = [];

   GetDoctor(){
    return this.serivces.GetAllDoctor().subscribe(res =>{
      this.Doctors=res;
      console.log(this.Doctors);
    })
   }
   
  

   status:any[]=[];

   getstatus(){
    return this.serivces.getStatus().subscribe(res=>{
      this.status=res;
      console.log(this.status);
    })
   }

   Receptionist: any[]=[];
   getRecep(){
    return this.serivces.getReceptionist().subscribe(res=>{
      this.Receptionist=res;
      console.log(this.Receptionist);
    })
   }



  

// AppointmentForm(){
 
// }

OnSubmit(){
  const appointmentData = this.appointmentFormPost.value;
  appointmentData.fees = Number(appointmentData.fees); // convert fees to number

  console.log(appointmentData);
  console.log(this.appointmentFormPost.value);
  //  this.PatientFormPost.value.isActive=="false"?false:true;
   this.serivces.CreateAppointment(this.appointmentFormPost.value).subscribe(
    (res :any) =>{
      setTimeout(() => {
        this.toastr.success('Submiited  Successfully ','Appoitment Added', {timeOut:2000, closeButton:true});
        }, 200);
      this._ngZone.run(()=>this.router.navigateByUrl('/view-appointments'));
   },
   err=>{
    this.router.navigate(['/home-page']);
  }
   )
  }   


OnLogoutClick(){
  this.loginService.logoutUser();
  this.router.navigate(['/home-page']);
}

}
