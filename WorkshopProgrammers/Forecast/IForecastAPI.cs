using System.Threading.Tasks;
using System.Collections.Generic;

namespace WorkshopProgrammers.Forecast
{
    public interface IForecastAPI
    {
        Task<List<ForecastResult>> GetForecast(string cityID);
    }
}