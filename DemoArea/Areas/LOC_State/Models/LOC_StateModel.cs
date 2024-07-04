using System;
namespace DemoArea.Areas.LOC_State.Models
{
	public class LOC_StateModel
	{
        public int StateID { get; set; }
        public int CountryID { get; set; }
        public string StateName { get; set; } = string.Empty;
        public string StateCode { get; set; } = string.Empty;
    }
}

