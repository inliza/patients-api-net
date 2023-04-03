using patients_api_net.Dto;
using patients_api_net.Models;
using patients_api_net.Repository.Patients.Interface;
using System.Data.SqlClient;

namespace patients_api_net.Repository.Patients
{
    public class PatientsRepo : IPatientsRepo
    {
        private readonly string _cnnString = "Server=wdb4.my-hosting-panel.com;Database=carrosf1_patients;User Id=carrosf1_usrPatients;Password=K8ch03@r0;";

        public PatientsRepo()
        {
        }
        public async Task AddPatient(PatientsModel patient)
        {
            using var cnn = new SqlConnection(_cnnString);
            using var cmd = new SqlCommand("INSERT INTO dbo.Patients (FirstName, LastName, Gender, Birthday, PhoneNumber, CreatedDate) VALUES (@firstName, @lastName, @gender, @birthday, @phoneNumber, @createdDate)", cnn);
            cmd.Parameters.AddWithValue("@firstName", patient.FirstName);
            cmd.Parameters.AddWithValue("@lastName", patient.LastName);
            cmd.Parameters.AddWithValue("@gender", patient.Gender);
            cmd.Parameters.AddWithValue("@birthday", patient.Birthday);
            cmd.Parameters.AddWithValue("@phoneNumber", patient.PhoneNumber);
            cmd.Parameters.AddWithValue("@createdDate", DateTime.Now);
            cnn.Open();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeletePatient(int id)
        {
            using var cnn = new SqlConnection(_cnnString);
            using var cmd = new SqlCommand("DELETE FROM Patients WHERE Id = @id", cnn);
            cmd.Parameters.AddWithValue("@id", id);
            cnn.Open();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<PatientsDTO> GetPatient(int id)
        {
            using var cnn = new SqlConnection(_cnnString);
            using var cmd = new SqlCommand("SELECT p.Id, p.FirstName, p.LastName, p.Gender, p.Birthday, p.PhoneNumber FROM Patients p WHERE p.Id = @id", cnn);
            cmd.Parameters.AddWithValue("@id", id);
            cnn.Open();
            using var reader = await cmd.ExecuteReaderAsync();
            if (reader.Read())
            {
                return new PatientsDTO(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetDateTime(4),
                    reader.GetString(5));
            }
            return null;
        }

        public async Task<List<PatientsDTO>> GetPatients()
        {
            using var cnn = new SqlConnection(_cnnString);
            using var cmd = new SqlCommand("SELECT p.Id, p.FirstName, p.LastName, p.Gender, p.Birthday, p.PhoneNumber FROM Patients p", cnn);
            var patients = new List<PatientsDTO>();
            cnn.Open();
            using var reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                patients.Add(new PatientsDTO(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetDateTime(4),
                    reader.GetString(5)));
            }
            return patients;
        }

        public async Task UpdatePatient(PatientsModel patient)
        {
            using var cnn = new SqlConnection(_cnnString);
            using var cmd = new SqlCommand("UPDATE  Patients SET FirstName = @firstName,  LastName = @lastName, Gender = @gender,  Birthday = @birthday, PhoneNumber = @phoneNumber, UpdatedDate = @updatedDate WHERE Id = @id", cnn);
            cmd.Parameters.AddWithValue("@id", patient.Id);
            cmd.Parameters.AddWithValue("@firstName", patient.FirstName);
            cmd.Parameters.AddWithValue("@lastName", patient.LastName);
            cmd.Parameters.AddWithValue("@gender", patient.Gender);
            cmd.Parameters.AddWithValue("@birthday", patient.Birthday);
            cmd.Parameters.AddWithValue("@phoneNumber", patient.PhoneNumber);
            cmd.Parameters.AddWithValue("@updatedDate", DateTime.Now);
            cnn.Open();
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
