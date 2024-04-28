import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Appointment } from './appointment';
import { CreatAppointment } from './creat-appointment';
import { Observable } from 'rxjs';
import { Doctors } from './Doctors';
import { status } from './status';
import { Prescription } from './prescription';
import { Patient } from './patient';
import { Receptionist } from './Receptionist';

@Injectable({
  providedIn: 'root'
})
export class AppointmentService {

  readonly baseURL= 'https://localhost:7213/api/Appointment/DoctorAppointments/';
  readonly CreateURL='https://localhost:7213/api/Appointment/CreateAppointment/';
  readonly DoctorUrl='https://localhost:7213/api/Doctor';
  readonly StatusURL='https://localhost:7213/api/AppointmentStatus';
  readonly PrescURL='https://localhost:7213/api/Prescription/CreatePrescrption';
  readonly PrescrpitonUrl='https://localhost:7213/api/Prescription/';
  readonly AppURl='https://localhost:7213/api/Appointment/AllAppointments';
  readonly ReceURL='https://localhost:7213/api/Receptionist';
  readonly UpdateURL='https://localhost:7213/api/Appointment/UpdateAppointment';

  constructor(private http:HttpClient) {}

  httpOptions={
    headers : new HttpHeaders({
      'content-type': 'application/json',
      'Authorization':"Bearer "+localStorage.getItem("token")
    })
  }
  // appointmentdata:CreatAppointment = new CreatAppointment();
 
 // Doctor Get Appointment 
  GetAppointmentData(): Observable<Appointment[]> {
    // console.log("Bearer " + localStorage.getItem("token"));
    return this.http.get<Appointment[]>(this.baseURL, this.httpOptions);
  }

  
   GetAllDoctor():Observable<Doctors[]>{
    return this.http.get<Doctors[]>(this.DoctorUrl,this.httpOptions);
   }

  //create by Receptionist 
  CreateAppointment(data:CreatAppointment):Observable<CreatAppointment[]>{
    console.log("Bearer "+localStorage.getItem("token"));
    return this.http.post<CreatAppointment[]>(this.CreateURL,data,this.httpOptions);
  }
   
  getAppointmentById(id: number): Observable<Appointment> {
    return this.http.get<Appointment>(this.baseURL + id,this.httpOptions);
  }

  getStatus():Observable<status[]>{
     return this.http.get<status[]>(this.StatusURL,this.httpOptions);
  }
   
  getPresceptionDetails():Observable<Prescription[]>{
    return this.http.get<Prescription[]>(this.PrescrpitonUrl,this.httpOptions);
  }


  CreatePrescrption(data:Prescription):Observable<Prescription[]>{
    console.log("Bearer "+localStorage.getItem("token"));
    return this.http.post<Prescription[]>(this.PrescURL,data,this.httpOptions);
  }
    updatePrescription(id:number,precrption:Prescription):Observable<Prescription>{
      console.log("Bearer "+localStorage.getItem("token"));
      return this.http.put<Prescription>(this.PrescrpitonUrl+id, JSON.stringify(precrption),this.httpOptions);
    }
   
  //get for prescription 
  getSinglePrescription(prescriptionId:number): Observable<Prescription>{
    return this.http.get<Prescription>(this.PrescrpitonUrl + prescriptionId, this.httpOptions);
  }

  // get All Appointment By Receptionist 
  getAllAppointments():Observable<Appointment[]>{
    return this.http.get<Appointment[]>(this.AppURl,this.httpOptions);
  }

// Get patient form id
  getReceptionist():Observable<Receptionist[]>{
    return this.http.get<Receptionist[]>(this.ReceURL,this.httpOptions);
  }

    updateAppointment(id :number):Observable<any>{
      return this.http.get<any>(this.UpdateURL+'?appointmentId='+id,this.httpOptions);
    }

}


