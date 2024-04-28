import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule }  from '@angular/platform-browser/animations';
import {HttpClientModule} from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { LoginService } from './shared/login.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PatientComponent } from './patient/patient.component';
import { PatientFormComponent } from './patient/patient-form/patient-form.component';
import { AppointmentsComponent } from './appointments/appointments.component';
import { AppointmentsFormComponent } from './appointments/appointments-form/appointments-form.component';
import { PrescrptionsComponent } from './prescription/prescrptions.component';
import { PrescrptionDetailsComponent } from './prescription/prescrption-details/prescrption-details.component';
import { EditPrescptionComponent } from './prescription/edit-prescption/edit-prescption.component';
import { ViewAppointmentComponent } from './appointments/view-appointment/view-appointment.component';
import { EditPatientComponent } from './patient/edit-patient/edit-patient.component';
import { ToastrModule } from 'ngx-toastr';
import { PatientLoginComponent } from './patient-login/patient-login.component';
import { PatientViewPrescriptionComponent } from './patient-login/patient-view-prescription/patient-view-prescription.component';
import { HomePageComponent } from './home-page/home-page.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { PaginationComponent } from './pagination/pagination.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    PatientComponent,
    PatientFormComponent,
    AppointmentsComponent,
    AppointmentsFormComponent,
    PrescrptionsComponent,
    PrescrptionDetailsComponent,
    EditPrescptionComponent,
    ViewAppointmentComponent,
    EditPatientComponent,
    PatientLoginComponent,
    PatientViewPrescriptionComponent,
    HomePageComponent,
    PaginationComponent
  ],
  imports: [
    NgxPaginationModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    Ng2SearchPipeModule,
    BrowserAnimationsModule,
    FormsModule,
    ToastrModule.forRoot({ timeOut: 2000 ,enableHtml: true ,positionClass: 'toast-top-right'
  })
   
  ],
  providers: [LoginService],
  bootstrap: [AppComponent]
})
export class AppModule { }
