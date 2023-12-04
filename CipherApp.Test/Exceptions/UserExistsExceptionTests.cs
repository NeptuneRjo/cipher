using CipherApp.BLL.Utilities.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherApp.Test.Exceptions
{
    public class UserExistsExceptionTests
    {
        [Fact]
        public void UserExistsException_ThrowsException()
        {
            Assert.ThrowsAsync<UserExistsException>(() => throw new UserExistsException());
        }

        [Fact]
        public void UserDoesNotExistException_ThrowsException_WithMessage() 
        {
            var ex = Assert.ThrowsAsync<UserExistsException>(() => throw new UserExistsException("Test"));

            Assert.Contains("Test", ex.Result.Message);
        }

        [Fact]
        public void UserDoesNotExistException_ThrowsException_WithInnerException()
        {
            var ex = Assert.ThrowsAsync<UserExistsException>(() => throw new UserExistsException("Test", new Exception()));

            Assert.IsType<Exception>(ex.Result.InnerException);
        }
    }
}
