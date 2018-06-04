using System;

namespace WorkshopProgrammers.ForecastAPI
{
    public class ForecastResult
    {
        public DateTime dia { get; set; }
        public string tempo { get; set; }
        public string minima { get; set; }
        public string maxima { get; set; }
    }
}