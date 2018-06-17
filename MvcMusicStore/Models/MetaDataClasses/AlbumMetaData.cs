using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;    //Add this library to use MetaData
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

//The file in a location separate to the corresponding Model class Album
//Same namespace
namespace MvcMusicStore.Models  //.MetaDataClasses
{
    //Manage to link both Album classes from Model and this one
    [ModelMetadataTypeAttribute(typeof(AlbumMetaData))]
    //Same definition of the Model class
    public partial class Album { }

    //You can only annotate properties that physically exist in the table
    public class AlbumMetaData
    {
        [HiddenInput(DisplayValue = false)]
        public int AlbumId { get; set; }

        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        [Display(Name = "Artist")]
        public int ArtistId { get; set; }

        [Required]
        public string Title { get; set; }

        //Don't apply formatting when people are typing in the field. Apply the currency format
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public double Price { get; set; }

        //You can apply many annotations
        [ReadOnly(true)]
        [DataType(DataType.Url)]
        public string AlbumArtUrl { get; set; }
    }
}
