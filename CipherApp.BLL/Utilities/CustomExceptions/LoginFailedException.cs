namespace CipherApp.BLL.Utilities.CustomExceptions
{
    public class LoginFailedException : Exception
    {
        public LoginFailedException()
        {
            
        }

        public LoginFailedException(string message) : base(message)
        {
            
        }

        public LoginFailedException(string message, Exception inner) : base(message, inner) 
        { 
        
        }
    }
}
