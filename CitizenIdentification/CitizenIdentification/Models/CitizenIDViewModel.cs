using System;
namespace CitizenIdentification.Models
{
    public class CitizenIDViewModel
    {
        public string Name { get; set; }
        public string QRCode { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string VaccinationDate1 { get; set; }
        public string VaccinationDate2 { get; set; }
    }
}
