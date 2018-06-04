using System.Threading.Tasks;
using System.Collections.Generic;

namespace WorkshopProgrammers.ForecastAPI
{
    public interface IForecastAPI
    {
        Task<List<ForecastResult>> GetForecast(string cityID);
    }
}