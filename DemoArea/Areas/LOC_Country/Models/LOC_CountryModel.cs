using System;
using System.ComponentModel.DataAnnotations;

namespace DemoArea.Areas.LOC_Country.Models
{
    public class LOC_CountryModel
    {
        


        public int? CountryId { get; set; }
        [Required]
        public string CountryName { get; set; }

        [Required]
        public string CountryCode { get; set; }

    }

    

}


