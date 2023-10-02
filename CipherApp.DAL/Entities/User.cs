namespace CipherApp.DAL.Entities
{
    using BCrypt.Net;

    public partial class User
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<ChatUser> ChatUsers { get; set; }

        public ICollection<Message> Messages { get; set; }
    }

    public partial class User
    {
        /// <summary>
        /// Encrypt the password of the user instance
        /// </summary>
        public void EncryptPassword() =>
            this.Password = BCrypt.HashPassword(this.Password, BCrypt.GenerateSalt());

        /// <summary>
        /// Check if the provided password matches the stored hashed password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ValidatePassword(string password) =>
            BCrypt.Verify(password, this.Password);
    }
}
