import { HttpClient,HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Patient } from './patient';
import { Observable } from 'rxjs';
import { ViewPrescrption } from './ViewPrescrption';

@Injectable({
  providedIn: 'root'
})
export class PatientService {

  readonly baseURL ="https://localhost:7213/api/Patient/";
  readonly GetURL = "https://localhost:7213/api/Patient/ReceptionistGet/";
  readonly PrepURl="https://localhost:7213/api/PatientLogin/";

  httpOptions={
    headers : new HttpHeaders({
      'content-type': 'application/json',
      'Authorization':"Bearer "+localStorage.getItem("token")
    })
  }
  constructor(private http: HttpClient) { }

  PatientData(data:Patient):Observable<Patient>{
    console.log("Bearer "+localStorage.getItem("token"));
    return this.http.post<Patient>(this.baseURL,data,this.httpOptions);
  }

  getPatientData(): Observable<Patient[]> {
    // console.log("Bearer " + localStorage.getItem("token"));
 
    return this.http.get<Patient[]>(this.baseURL, this.httpOptions);
  }

  getPatientById(id:number):Observable<Patient>{
    return this.http.get<Patient>(this.GetURL+id,this.httpOptions);
   }

   UpdatePatient(id:number , Patient:Patient):Observable<Patient>{
    return this.http.put<Patient>(this.baseURL+id,Patient,this.httpOptions);
   }
  
  getPatientPrescrption(firstName:string):Observable<ViewPrescrption>{
    return this.http.get<ViewPrescrption>(this.PrepURl+firstName+`/prescrptions`);
  }
  getPatientByName(name: string): Observable<Patient[]> {
    return this.http.get<Patient[]>(`${this.baseURL}/search?name=${name}`);
  }

  searchPatients(firstName: string): Observable<Patient[]> {
    const searchUrl = `${this.baseURL}?name_like=${firstName}`;
    return this.http.get<Patient[]>(searchUrl,this.httpOptions);
  }

   getPatientFilter(page:number,pageSize:number):Observable<any>{
    const params=new HttpParams()
    .set('page',page.toString())
    .set('pageSize',pageSize.toString());

    return this.http.get<Patient[]>(`${this.baseURL}PatientFilter/`,{params});
   }
  

   }


