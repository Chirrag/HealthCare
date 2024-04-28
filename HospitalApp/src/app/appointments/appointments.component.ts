import { Component, OnInit } from '@angular/core';
import { AppointmentService } from '../shared/appointment.service';
import { LoginService } from '../shared/login.service';
import { Router } from '@angular/router';
import { CreatAppointment } from '../shared/creat-appointment';
import jwt_decode from "jwt-decode";
@Component({
  selector: 'app-appointments',
  templateUrl: './appointments.component.html',
  styleUrls: ['./appointments.component.css']
})
export class AppointmentsComponent implements OnInit {
    
   constructor(private services: AppointmentService,private loginService: LoginService,private router:Router)
   {}
  ngOnInit() {
    this.getAllAppointments();
    var code= jwt_decode(localStorage.getItem("token"));
   var check =code["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    if(check==="Doctor"){
   this.router.navigate(['/appointments']);
     } else{
        this.router.navigate(['/loginForm']);
      }
    }
  
  
   
  // PopulateList(selectedRecord:CreatAppointment){
  // console.log(  this.srvices.appointmentdata=Object.assign({},selectedRecord));
  // }

  // selectedAppointment: any;

  // onAddPrescriptionClick(appointment: any) {
  //    this.selectedAppointment = appointment;
  //   this.router.navigate(['/prescrptions', this.selectedAppointment.patientAppointmentId]);
  // }


  OnLogoutClick(){
    this.loginService.logoutUser();
    this.router.navigate(['/home-page']);
  }
   Appointmentlist :any[];

   getAllAppointments(){
    return this.services.GetAppointmentData().subscribe(res => {
      this.Appointmentlist = res;
      console.log(this.Appointmentlist)
    },
    err=>{
      console.log("hey bro "+err)
      this.router.navigateByUrl('/home-page');
    });
    
  }
}
