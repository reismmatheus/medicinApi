using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicinApi.Utils
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class EmptyListAttribute : ValidationAttribute
    {
        public EmptyListAttribute()
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;

            var list = value as List<string>;

            if(list == null || list.Count == 0)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
