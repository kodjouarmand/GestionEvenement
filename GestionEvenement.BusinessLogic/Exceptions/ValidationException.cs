using System;
using System.Collections.Generic;
using System.Text;

namespace GestionEvenement.BusinessLogic.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
