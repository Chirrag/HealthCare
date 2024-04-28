import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginService } from '../shared/login.service';
import { Router } from '@angular/router';
import jwt_decode from "jwt-decode";
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{

  loginForm: FormGroup;

 
constructor(private loginService:LoginService, 
  private form:FormBuilder,
  private router:Router,
  private toastr:ToastrService
  
  ) {
 
  
}
   ngOnInit(){
  this.loginForm = this.form.group({
    username: ['', Validators.required],
    password: ['', Validators.required],
  });

  
 }

  
  OnLogoutClick(){
    this.loginService.logoutUser();
    this.router.navigate(['/loginForm']);
  }

   OnSubmit(){
    console.log(this.loginForm.value);
     this.loginService.loginUser(this.loginForm.value).subscribe(
      (res :any) =>{
        console.log(res);
        localStorage.setItem("token",res.token);
        const decodedHeader = jwt_decode(res['token']);
        if(decodedHeader['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']=="Receptionist") {
        setTimeout(() => {
          this.toastr.info('Cj Hospital','Welcome Receptionist', {timeOut:2000, closeButton:true});
          }, 200);
        this.router.navigateByUrl('/patient');
        }
        else if (decodedHeader['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']=="Doctor")
       {
        setTimeout(() => {
          this.toastr.info('Cj Hospital','Welcome Doctor', {timeOut:2000, closeButton:true});
          }, 200);
          this.router.navigateByUrl('/appointments');
       }
        else
        this.router.navigateByUrl('/'); 
      }
     )
   }
 
}


