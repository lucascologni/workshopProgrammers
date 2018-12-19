using System;

namespace WorkshopProgrammers.Forecast
{
    public class ForecastResult
    {
        public DateTime Dia { get; set; }
        public string Tempo { get; set; }
        public string Minima { get; set; }
        public string Maxima { get; set; }
        public string LinkImagemClima { get; set; }
        public string LinkImagemBackground { get; set; }
    }
}