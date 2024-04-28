import { Component, OnInit } from '@angular/core';
import { AppointmentService } from 'src/app/shared/appointment.service';
import jwt_decode from "jwt-decode";
import { Router } from '@angular/router';
import { LoginService } from 'src/app/shared/login.service';
@Component({
  selector: 'app-prescrption-details',
  templateUrl: './prescrption-details.component.html',
  styleUrls: ['./prescrption-details.component.css']
})
export class PrescrptionDetailsComponent implements OnInit {
  
  Sno:number=1;
 
  constructor(private services: AppointmentService,
    private route:Router,
    private loginService:LoginService
    ){
   this.GetPrecrptions();
  }
  ngOnInit(): void {
    var code= jwt_decode(localStorage.getItem("token"));
   var check =code["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    if(check==="Receptionist"){
   this.route.navigate(['/prescrption-details']);
     } else{
        this.route.navigate(['/loginForm']);
      }
  }
  precrption: any[]=[];
  p:number =1;
  totalItems=this.precrption.length;

  GetPrecrptions(){
    return this.services.getPresceptionDetails().subscribe(res=>{
      this.precrption=res;
      console.log(this.precrption);
    },
    err=>{
      this.route.navigate(['/home-page']);
    })
}

OnLogoutClick(){
  this.loginService.logoutUser();
  this.route.navigate(['/loginForm']);
}
}
