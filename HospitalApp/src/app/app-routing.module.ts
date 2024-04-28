import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { PatientComponent } from './patient/patient.component';
import { AppointmentsComponent } from './appointments/appointments.component';
import { PatientFormComponent } from './patient/patient-form/patient-form.component';
import { AppointmentsFormComponent } from './appointments/appointments-form/appointments-form.component';
import { PrescrptionsComponent } from './prescription/prescrptions.component';
import { PrescrptionDetailsComponent } from './prescription/prescrption-details/prescrption-details.component';
import { EditPrescptionComponent } from './prescription/edit-prescption/edit-prescption.component';
import { ViewAppointmentComponent } from './appointments/view-appointment/view-appointment.component';
import { EditPatientComponent } from './patient/edit-patient/edit-patient.component';
import { PatientLoginComponent } from './patient-login/patient-login.component';
import { PatientViewPrescriptionComponent } from './patient-login/patient-view-prescription/patient-view-prescription.component';
import { HomePageComponent } from './home-page/home-page.component';

const routes: Routes = [
  { path: '', component:HomePageComponent, pathMatch:'full'},
  {path:'patient',component:PatientComponent  },
  {path:'appointments',component:AppointmentsComponent},
  {path:'patient-form',component:PatientFormComponent},
  {path:'appoinment-form/:id',component:AppointmentsFormComponent},
  { path: 'prescrptions/:id',component:PrescrptionsComponent },
  {path:'prescrption-details',component:PrescrptionDetailsComponent},
  {path:'edit/:id',component:EditPrescptionComponent},
  {path:'view-appointments',component:ViewAppointmentComponent},
  {path:'loginForm',component:LoginComponent},
  {path:'editpatient/:id',component:EditPatientComponent},
  {path:'patient-login',component:PatientLoginComponent},
  {path:'patient-view-prescription',component:PatientViewPrescriptionComponent},
     {path:'home-page',component:HomePageComponent}
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
