using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SiteClassLibrary
{   //can have only one class 
    public class PostalCodeAttribute: ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Regex pattern = new Regex(@"[a-z]\d[a-z]?\d[a-z]\d", RegexOptions.IgnoreCase);

            if (value != null || pattern.IsMatch(value.ToString()))
                return ValidationResult.Success;
            else
                return new ValidationResult($"{validationContext.DisplayName} is not a CDN pattern: A3A 3A3");
        }

    }
}
