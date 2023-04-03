namespace patients_api_net.Dto
{
    public class PatientsDTO
    {
        public PatientsDTO(int id, string firstName, string lastName, string gender, DateTime birthday, string phoneNumber)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Birthday = birthday;
            PhoneNumber = phoneNumber;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }

        public DateTime Birthday { get; set; }

        public string PhoneNumber { get; set; }

    }
}
