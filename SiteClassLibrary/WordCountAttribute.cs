using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SiteClassLibrary
{   //can have many classes
    public class WordCountAttribute: ValidationAttribute   
    {
        private Int32 MaxWords, MinWords = 0;

        public WordCountAttribute (Int32 maxWords)
        {
            ErrorMessage = "{0} cannot be longer than {1} words.";
            MaxWords = maxWords;
        }

        public WordCountAttribute (Int32 maxWords, Int32 minWords)
        {
            ErrorMessage = "{0} cannot be less than {2} or more than {2} wors";
            MaxWords = maxWords;
            MinWords = minWords;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && (value.ToString().Split(' ').Length > MaxWords) || (value.ToString().Split(' ').Length < MinWords))
            { 
                return new ValidationResult(string.Format(ErrorMessage, validationContext.DisplayName, MaxWords, MinWords));
            }
            else
                return ValidationResult.Success;
        }

    }
}
