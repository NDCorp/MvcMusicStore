using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using SiteClassLibrary;

namespace MvcMusicStore.Models
{
    [ModelMetadataTypeAttribute(typeof(OrderMetaData))]
    public partial class Order : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //Reset the field or do some manipulations before to write to the DB: last line of server side validation, before you write to the DB
            if (string.IsNullOrEmpty(FirstName.Trim()))
                yield return new ValidationResult("First Name cannot be empty", new[] {"FirstName"});
            else
                FirstName = FirstName.Trim();
        }
    }

    public class OrderMetaData
    {
        public int OrderId { get; set; }

        [Remote("OrderDateNotInFuture", "Remotes")]    //client side validation code created: it's going to generate the JS for you
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dd MMM YYYY}")]
        public DateTime OrderDate { get; set; }

        //Annotation for multiple fields. This 3 fields will be sent to the RemotesController with Get method
        [Remote("CheckUserName", "Remotes", AdditionalFields = "FirstName, LastName")]
        public string UserName { get; set; }

        [Display(Name = "First Name")]
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string LastName { get; set; }
        public string Address { get; set; }

        //You can create a many validations in the same file WordCountAttribute
        [WordCount(5, 2)]   //server side annotation
        public string City { get; set; }
        public string ProvinceCode { get; set; }

        //[RegularExpression(@"^[A-Za-z]\d[A-Za-z] ?d[A-Za-z]\d$")]
        [PostalCode]    //server side annotation
        public string PostalCode { get; set; }

        [RegularExpression(@"^[A-Za-z][A-Za-z]$")]
        public string CountryCode { get; set; }

        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$")]
        public string Phone { get; set; }
        public string Email { get; set; }
        
        [Range(0, 100)]
        public double Total { get; set; }

    }
}
