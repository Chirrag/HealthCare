import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Patient } from '../shared/patient';
import { PatientService } from '../shared/patient.service';
import { LoginService } from '../shared/login.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs';



@Component({
  selector: 'app-patient',
  templateUrl: './patient.component.html',
  styleUrls: ['./patient.component.css']
})
export class PatientComponent  implements OnInit{
 ;
  // searchForm: FormGroup;
  filteredPatientList: Patient[] = [];
  Patient:any[]= [];
  pages: number[] = []; 
  page=1;
  pageSize=10;
 

  //  nextPage():void{
  //   this.pageChange.emit(this.page+1);
  //  }
  //  prevPage():void {
  //   if(this.page>1){
  //     this.pageChange.emit(this.page-1);
  //   }
  //  }



  constructor(private patientService: PatientService, 
    private service:LoginService,
    private router:Router,
    private toastr:ToastrService,
    private fb:FormBuilder
    ){}

    ngOnInit(){
       this.loadPatient();
       this.getAllPatients();
       
     }
   
  Sno=1;
  totalItem=0; 
    
  patientList: any = []
  searchText;
  totalItems=this.patientList.length;
 

  getAllPatients(){
    return this.patientService.getPatientData().subscribe(res => {
      //  this.patientList = res;
     this.filteredPatientList = res;
      console.log(this.patientList)
    },
    err=>{
      this.router.navigateByUrl('/home-page');
    }
    )
  }
  
   loadPatient():void{
    this.patientService.getPatientFilter(this.page,this.pageSize)
    .subscribe(data=>{
      console.log(data);
      this.Patient=data;

     
    })
    }
  // OnPageChange(newPage:number):void{
  //   if(newPage >=1 && newPage <=this.pages.length)
  //   this.page=newPage;
  //   this.loadPatient();
  // }

  OnLogoutClick(){
    this.service.logoutUser();
    this.router.navigate(['/home-page']);
  }

  
    }

