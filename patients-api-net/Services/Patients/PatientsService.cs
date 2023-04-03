using patients_api_net.Helpers;
using patients_api_net.Models;
using patients_api_net.Repository.Patients.Interface;
using patients_api_net.Services.Patients.Interface;

namespace patients_api_net.Services.Patients
{
    public class PatientsService : IPatientsService
    {
        private readonly IPatientsRepo _patientsRepo;
        public PatientsService(IPatientsRepo patientsRepo)
        {
            _patientsRepo = patientsRepo;
        }
        public async Task<ServicesResponse> AddPatient(PatientsModel patient)
        {
            try
            {
                if (patient == null)
                {
                    return new ServicesResponse(400, "Invalid payload", patient);
                }
                await _patientsRepo.AddPatient(patient);
                return new ServicesResponse(200, "Patient added correctly", patient);
            }
            catch (Exception ex)
            {
                return new ServicesResponse(500, "An error occurred adding the patient", new { error = ex.Message });
            }
        }

        public async Task<ServicesResponse> DeletePatient(int id)
        {
            try
            {
                var patient = await _patientsRepo.GetPatient(id);
                if (patient == null)
                {
                    return new ServicesResponse(404, "This patient does not exist", null);
                }
                await _patientsRepo.DeletePatient(id);
                return new ServicesResponse(200, "Patient deleted correctly", id);
            }
            catch (Exception ex)
            {
                return new ServicesResponse(500, "An error occurred deleting the patient", new { error = ex.Message });
            }
        }

        public async Task<ServicesResponse> GetPatient(int id)
        {
            try
            {
                var patient = await _patientsRepo.GetPatient(id);
                if (patient == null)
                {
                    return new ServicesResponse(404, "This patient does not exist", id);
                }
                return new ServicesResponse(200, "", patient);
            }
            catch (Exception ex)
            {
                return new ServicesResponse(500, "An error occurred getting the patient", new { error = ex.Message });
            }

        }

        public async Task<ServicesResponse> GetPatients()
        {
            try
            {
                var patients = await _patientsRepo.GetPatients();
                return new ServicesResponse(200, "", patients);
            }
            catch (Exception ex)
            {
                return new ServicesResponse(500, "An error occurred getting the patients", new { error = ex.Message });
            }

        }

        public async Task<ServicesResponse> UpdatePatient(PatientsModel patient)
        {
            try
            {
                var patientExist = await _patientsRepo.GetPatient(patient.Id);
                if (patientExist == null)
                {
                    return new ServicesResponse(404, "This patient does not exist", patient);
                }
                await _patientsRepo.UpdatePatient(patient);
                return new ServicesResponse(200, "Patient updated correctly", patient);
            }
            catch (Exception ex)
            {
                return new ServicesResponse(500, "An error occurred updated the patient", new { error = ex.Message });
            }
        }
    }
}
