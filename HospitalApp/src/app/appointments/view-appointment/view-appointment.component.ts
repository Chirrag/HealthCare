import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AppointmentService } from 'src/app/shared/appointment.service';
import jwt_decode from "jwt-decode";
import { LoginService } from 'src/app/shared/login.service';
@Component({
  selector: 'app-view-appointment',
  templateUrl: './view-appointment.component.html',
  styleUrls: ['./view-appointment.component.css']
})
export class ViewAppointmentComponent implements OnInit{
  
   public text: string= 'Active';
  constructor(private services: AppointmentService ,
    private router:Router,
    private route:ActivatedRoute,
    private loginservice:LoginService
    ){
      this.updateAppointment;
  }
  ngOnInit(): void {
    var code= jwt_decode(localStorage.getItem("token"));
    var check =code["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
     if(check==="Receptionist"){
    this.router.navigate(['/view-appointments']);
      } else{
         this.router.navigate(['/loginForm']);
       }
    
    this.getAllAppointment();
  }

  p : number=1;
  searchText;
  Appointmentlist :any[];

 
  getAllAppointment(){
   return this.services.getAllAppointments().subscribe(res => {
     this.Appointmentlist = res;
     console.log(this.Appointmentlist)
   })
 }

   updateAppointment(appointmentId: number){
  //   const id=this.route.snapshot.paramMap['id'];
  console.log(`number==${appointmentId}`);
  if(confirm('Are you Sure to Deactive  this Record?'))
  {
    this.services.updateAppointment(appointmentId)
    .subscribe(res=>{
      console.log("oh ok");
      window.location.reload();
      
    })

   }
  }
   OnLogoutClick(){
    this.loginservice.logoutUser();
    this.router.navigate(['/home-page']);
  }
  //  }
}
