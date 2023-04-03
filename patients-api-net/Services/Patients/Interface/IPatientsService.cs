using patients_api_net.Dto;
using patients_api_net.Helpers;
using patients_api_net.Models;

namespace patients_api_net.Services.Patients.Interface
{
    public interface IPatientsService
    {
        Task<ServicesResponse> GetPatients();
        Task<ServicesResponse> GetPatient(int id);
        Task<ServicesResponse> AddPatient(PatientsModel patient);
        Task<ServicesResponse> UpdatePatient(PatientsModel patient);
        Task<ServicesResponse> DeletePatient(int id);
    }
}
