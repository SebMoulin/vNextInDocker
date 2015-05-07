namespace TEK.Recruit.Commons.Dtos
{
    public class CandidateProfileDto
    {
        public string ProjectId { get; set; }

        public string CandidateId { get; set; }

        public string DevEnv { get; set; }

        public string Position { get; set; }

        public string TEKCenter { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }

        public AddressDto Address { get; set; }
    }
}