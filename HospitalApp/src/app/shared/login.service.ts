import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Login } from './login';
import { Observable } from 'rxjs';
import { PatientLogin } from './patientLogin';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  readonly LoginBaseUrl= "https://localhost:7213/api/Authentication/login";
  readonly PatientLoginUrl="https://localhost:7213/api/PatientLogin/login";


  httpOptions={
    headers : new HttpHeaders({
      'content-type': 'application/json',
      'Authorization':"Bearer "+localStorage.getItem("token")
    })
  }
  

  constructor(private http: HttpClient) { }
    

  logoutUser(): void{
    localStorage.removeItem('token');
   }

   loginUser(data : Login):Observable<Login> {
    return this.http.post<Login>(this.LoginBaseUrl,data,this.httpOptions);
   }

   patientLogin(data:PatientLogin):Observable<PatientLogin>{
    return this.http.post<PatientLogin>(this.PatientLoginUrl,data);
   }

   
}
