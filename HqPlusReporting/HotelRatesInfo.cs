using System.Collections.Generic;

namespace HqPlusReporting
{
    public class HotelRatesInfo
    {
        public Hotel Hotel { get; set; }
        public ICollection<HotelRate> HotelRates { get; set; }
    }
}
