using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJTrailer.Library
{
    public class Address
    {
        public Guid ID { get; set; }
        public int StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string UnitNumber { get; set; }
        public string CityName { get; set; }
        public string ProvinceName { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

    }
}
