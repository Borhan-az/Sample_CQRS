using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSWebAPI.Validation
{
    public interface IValidationHandler { }

    public interface IValidationHandler<T> : IValidationHandler
    {
        Task<ValidationResult> Validation(T request);
    }
}