﻿using patients_api_net.Dto;
using patients_api_net.Models;
using patients_api_net.Repository.Patients.Interface;
using System.Data.SqlClient;

namespace patients_api_net.Repository.Patients
{
    public class PatientsRepo : IPatientsRepo
    {
        private readonly string _cnnString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Patients;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

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
            using var cmd = new SqlCommand("UPDATE  Patients SET FirstName = @firstName,  LastName = @lastName, Gender = @gender,  Birthday = @birthday, PhoneNumber = @phoneNumber, UpdatedDate = @updatedDate", cnn);
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
