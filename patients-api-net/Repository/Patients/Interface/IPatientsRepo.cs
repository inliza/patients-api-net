using patients_api_net.Dto;
using patients_api_net.Models;

namespace patients_api_net.Repository.Patients.Interface
{
    public interface IPatientsRepo
    {
        Task<List<PatientsDTO>> GetPatients();
        Task<PatientsDTO> GetPatient(int id);
        Task AddPatient(PatientsModel patient);
        Task UpdatePatient(PatientsModel patient);
        Task DeletePatient(int id);
    }
}
