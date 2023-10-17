﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherApp.BLL.Utilities.CustomExceptions
{
    public class UserExistsException : Exception
    {
        public UserExistsException() { }

        public UserExistsException(string message): base(message) { }
    
        public UserExistsException(string message, Exception inner) : base(message, inner) { }
    }
}
