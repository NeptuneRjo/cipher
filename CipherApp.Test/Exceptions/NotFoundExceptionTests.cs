using CipherApp.BLL.Utilities.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherApp.Test.Exceptions
{
    public class NotFoundExceptionTests
    {
        [Fact]
        public void NotFoundException_ThrowsException()
        {
            Assert.ThrowsAsync<NotFoundException>(() => throw new NotFoundException());
        }

        [Fact]
        public void NotFoundException_ThrowsException_WithMessage()
        {
            var ex = Assert.ThrowsAsync<NotFoundException>(()=> throw new NotFoundException("Test"));

            Assert.Contains("Test", ex.Result.Message);
        }

        [Fact]
        public void NotFoundException_ThrowsException_WithInnerException()
        {
            var ex = Assert.ThrowsAsync<NotFoundException>(() => throw new NotFoundException("Test", new Exception()));

            Assert.IsType<Exception>(ex.Result.InnerException);
        }
    }
}
