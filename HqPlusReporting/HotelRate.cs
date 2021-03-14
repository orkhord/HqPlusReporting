using System;
using System.Collections.Generic;

namespace HqPlusReporting
{
    public class HotelRate
    {
        public int Adults { get; set; }
        public int Los { get; set; }
        public Price Price { get; set; }
        public string RateDescription { get; set; }
        public string RateId { get; set; }
        public string RateName { get; set; }
        public ICollection<RateTag> RateTags { get; set; }
        public DateTimeOffset TargetDay { get; set; }
    }
}
