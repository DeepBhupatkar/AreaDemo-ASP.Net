using System;
namespace DemoArea.Areas.LOC_City.Models
{
	public class LOC_CityModel
	{
        public int CityID { get; set; }
        public string CityName { get; set; } = string.Empty;
        public string CityCode { get; set; } = string.Empty;
        public int StateID { get; set; }
        public int CountryID { get; set; }
    }
}

