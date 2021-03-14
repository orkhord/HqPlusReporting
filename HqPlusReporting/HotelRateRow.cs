using System;
using System.Linq;

namespace HqPlusReporting
{
    public class HotelRateRow
    {
        public string ArrivalDay { get; set; }
        public string DepartureDay { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
        public string Name { get; set; }
        public int Adults { get; set; }
        public string Breakfast { get; set; }
        public string Id { get; set; }
        public string Description { get; set; }
        public HotelRateRow(HotelRate data)
        {
            Adults = data.Adults;
            Price = data.Price.NumericFloat;
            Currency = data.Price.Currency;
            Description = data.RateDescription;
            Id = data.RateId;
            Name = data.RateName;
            var breakfastTag = data.RateTags.SingleOrDefault(t => t.Name == "breakfast");
            if (breakfastTag != null)
            {
                Breakfast = breakfastTag.Shape ? "Included" : "Excluded";
            }
            ArrivalDay = data.TargetDay.ToString("d");
            DepartureDay = data.TargetDay.AddDays(data.Los).ToString("d");
        }
    }
}
