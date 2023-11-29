using CipherApp.BLL.Utilities.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherApp.Test.Exceptions
{
    public class LoginFailedExceptionTests
    {
        [Fact]
        public void LoginFailedException_ThrowsException()
        {
            Assert.ThrowsAsync<LoginFailedException>(() => throw new LoginFailedException());
        }

        [Fact]
        public void LoginFailedException_ThrowsException_WithMessage()
        {
            var ex = Assert.ThrowsAsync<LoginFailedException>(() => throw new LoginFailedException("Test"));

            Assert.Contains("Test", ex.Result.Message);
        }

        [Fact]
        public void LoginFailedException_ThrowsException_WithInnerException()
        {
            var ex = Assert.ThrowsAsync<LoginFailedException>(() => throw new LoginFailedException("Test", new Exception()));

            Assert.IsType<Exception>(ex.Result.InnerException);
        }
    }
}
