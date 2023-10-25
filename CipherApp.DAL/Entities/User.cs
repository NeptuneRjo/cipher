namespace CipherApp.DAL.Entities
{
    using BCrypt.Net;

    public partial class User
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Email { get; set; }
        public string UID { get; set; }

        public User()
        {
            CreatedAt = DateTime.Now;
            Password = EncryptPassword(Password);
        }
    }

    public partial class User
    {
        /// <summary>
        /// Encrypt the password of the user instance
        /// </summary>
        public string EncryptPassword(string password) =>
            BCrypt.HashPassword(password, BCrypt.GenerateSalt());

        /// <summary>
        /// Check if the provided password matches the stored hashed password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ValidatePassword(string password) =>
            BCrypt.Verify(password, this.Password);
    }
}
