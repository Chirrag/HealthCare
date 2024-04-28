using HospitalManagement.Core.UseCase.Appointments;
using HospitalManagement.Core.UseCase.Patient;
using HospitalMangement.Domain.Contacts;
using HospitalMangement.Infrastructure.Repository;
using HospitalMangment.Domain.Contacts;
using HospitalMangment.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;


namespace HospitalMangement.IOC.Startup
{
    public class DependencyInjection
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IReceptionistRepository, ReceptionistRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
            services.AddScoped<IAppointmentStatusRepository, AppointmentStatusRepository>();


            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IPatientService, PatientService>();


           
        }
    }
}
