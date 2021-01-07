

namespace IIAADataModels.Transfer
{
	public class Therapist
	{
        public string Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string ImageUrl { get; set; }
        public string Salt { get; set; }
    }
}
